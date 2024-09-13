using Application.Features.LogManagementFeatures.LogEmailSends.Constants;
using Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogEmailSends.Rules;
using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Commands.Update;

public class UpdateLogEmailSendCommand : IRequest<UpdatedLogEmailSendResponse>
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }



    public class UpdateLogEmailSendCommandHandler : IRequestHandler<UpdateLogEmailSendCommand, UpdatedLogEmailSendResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogEmailSendWriteRepository _logEmailSendWriteRepository;
        private readonly ILogEmailSendReadRepository _logEmailSendReadRepository;
        private readonly LogEmailSendBusinessRules _logEmailSendBusinessRules;

        public UpdateLogEmailSendCommandHandler(IMapper mapper, ILogEmailSendWriteRepository logEmailSendWriteRepository,
                                         LogEmailSendBusinessRules logEmailSendBusinessRules, ILogEmailSendReadRepository logEmailSendReadRepository)
        {
            _mapper = mapper;
            _logEmailSendWriteRepository = logEmailSendWriteRepository;
            _logEmailSendBusinessRules = logEmailSendBusinessRules;
            _logEmailSendReadRepository = logEmailSendReadRepository;
        }

        public async Task<UpdatedLogEmailSendResponse> Handle(UpdateLogEmailSendCommand request, CancellationToken cancellationToken)
        {
            X.LogEmailSend? logEmailSend = await _logEmailSendReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _logEmailSendBusinessRules.LogEmailSendShouldExistWhenSelected(logEmailSend);
            await _logEmailSendBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);

            logEmailSend = _mapper.Map(request, logEmailSend);

            _logEmailSendWriteRepository.Update(logEmailSend!);
            await _logEmailSendWriteRepository.SaveAsync();

            X.LogEmailSend? savedLogEmailSend = await _logEmailSendReadRepository.GetAsync(predicate: x => x.Gid == logEmailSend.Gid, include: x => x.Include(x => x.UserFK));

            GetByGidLogEmailSendResponse obj = _mapper.Map<GetByGidLogEmailSendResponse>(logEmailSend);

            return new()
            {
                Title = LogEmailSendsBusinessMessages.ProcessCompleted,
                Message = LogEmailSendsBusinessMessages.SuccessCreatedLogEmailSendMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}