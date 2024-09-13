using Application.Features.LogManagementFeatures.LogFailedLogins.Constants;
using Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogFailedLogins.Rules;
using Application.Repositories.LogManagementRepos.LogFailedLoginRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Update;

public class UpdateLogFailedLoginCommand : IRequest<UpdatedLogFailedLoginResponse>
{
    public Guid Gid { get; set; }

	
public string Email { get; set; }
public string Password { get; set; }
public string? IpAddress { get; set; }
public string? Description { get; set; }



    public class UpdateLogFailedLoginCommandHandler : IRequestHandler<UpdateLogFailedLoginCommand, UpdatedLogFailedLoginResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogFailedLoginWriteRepository _logFailedLoginWriteRepository;
        private readonly ILogFailedLoginReadRepository _logFailedLoginReadRepository;
        private readonly LogFailedLoginBusinessRules _logFailedLoginBusinessRules;

        public UpdateLogFailedLoginCommandHandler(IMapper mapper, ILogFailedLoginWriteRepository logFailedLoginWriteRepository,
                                         LogFailedLoginBusinessRules logFailedLoginBusinessRules, ILogFailedLoginReadRepository logFailedLoginReadRepository)
        {
            _mapper = mapper;
            _logFailedLoginWriteRepository = logFailedLoginWriteRepository;
            _logFailedLoginBusinessRules = logFailedLoginBusinessRules;
            _logFailedLoginReadRepository = logFailedLoginReadRepository;
        }

        public async Task<UpdatedLogFailedLoginResponse> Handle(UpdateLogFailedLoginCommand request, CancellationToken cancellationToken)
        {
            X.LogFailedLogin? logFailedLogin = await _logFailedLoginReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _logFailedLoginBusinessRules.LogFailedLoginShouldExistWhenSelected(logFailedLogin);
            logFailedLogin = _mapper.Map(request, logFailedLogin);

            _logFailedLoginWriteRepository.Update(logFailedLogin!);
            await _logFailedLoginWriteRepository.SaveAsync();
            GetByGidLogFailedLoginResponse obj = _mapper.Map<GetByGidLogFailedLoginResponse>(logFailedLogin);

            return new()
            {
                Title = LogFailedLoginsBusinessMessages.ProcessCompleted,
                Message = LogFailedLoginsBusinessMessages.SuccessUpdatedLogFailedLoginMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}