using Application.Features.LogManagementFeatures.LogSuccessedLogins.Constants;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Rules;
using Application.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using AutoMapper;
using Domain.Entities.LogManagements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.LogOutLog
{
    public class LogOutLogSuccessedLoginCommand : IRequest<LogOutLogSuccessedLoginResponse>
    {
        public Guid SucceededLogGid { get; set; }
        public string SessionId { get; set; }

        public class LogOutLogSuccessedLoginCommandHandler : IRequestHandler<LogOutLogSuccessedLoginCommand, LogOutLogSuccessedLoginResponse>
        {
            private readonly IMapper _mapper;
            private readonly ILogSuccessedLoginWriteRepository _logSuccessedLoginWriteRepository;
            private readonly ILogSuccessedLoginReadRepository _logSuccessedLoginReadRepository;
            private readonly LogSuccessedLoginBusinessRules _logSuccessedLoginBusinessRules;

            public LogOutLogSuccessedLoginCommandHandler(IMapper mapper, ILogSuccessedLoginWriteRepository logSuccessedLoginWriteRepository, ILogSuccessedLoginReadRepository logSuccessedLoginReadRepository, LogSuccessedLoginBusinessRules logSuccessedLoginBusinessRules)
            {
                _mapper = mapper;
                _logSuccessedLoginWriteRepository = logSuccessedLoginWriteRepository;
                _logSuccessedLoginReadRepository = logSuccessedLoginReadRepository;
                _logSuccessedLoginBusinessRules = logSuccessedLoginBusinessRules;
            }

            public async Task<LogOutLogSuccessedLoginResponse> Handle(LogOutLogSuccessedLoginCommand request, CancellationToken cancellationToken)
            {
                LogSuccessedLogin? logSuccessedLogin = await _logSuccessedLoginReadRepository.GetAsync(predicate: x => x.Gid == request.SucceededLogGid && x.SessionId == request.SessionId, cancellationToken: cancellationToken);
                await _logSuccessedLoginBusinessRules.LogSuccessedLoginShouldExistWhenSelected(logSuccessedLogin);

                logSuccessedLogin = _mapper.Map(request, logSuccessedLogin);
                logSuccessedLogin.LogOutDate = DateTime.Now;
                _logSuccessedLoginWriteRepository.Update(logSuccessedLogin!);
                await _logSuccessedLoginWriteRepository.SaveAsync();
                LogOutLogSuccessedLoginResponse obj = _mapper.Map<LogOutLogSuccessedLoginResponse>(logSuccessedLogin);
                
                return new()
                {
                    Title = LogSuccessedLoginsBusinessMessages.ProcessCompleted,
                    Message = LogSuccessedLoginsBusinessMessages.SuccessfulLogout,
                    IsValid = true,
                };


            }
        }
    }
}
