using Application.Features.LogManagementFeatures.LogEmailSends.Constants;
using Application.Features.LogManagementFeatures.LogEmailSends.Rules;
using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Commands.Delete;

public class DeleteLogEmailSendCommand : IRequest<DeletedLogEmailSendResponse>
{
	public Guid Gid { get; set; }

    public class DeleteLogEmailSendCommandHandler : IRequestHandler<DeleteLogEmailSendCommand, DeletedLogEmailSendResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogEmailSendReadRepository _logEmailSendReadRepository;
        private readonly ILogEmailSendWriteRepository _logEmailSendWriteRepository;
        private readonly LogEmailSendBusinessRules _logEmailSendBusinessRules;

        public DeleteLogEmailSendCommandHandler(IMapper mapper, ILogEmailSendReadRepository logEmailSendReadRepository,
                                         LogEmailSendBusinessRules logEmailSendBusinessRules, ILogEmailSendWriteRepository logEmailSendWriteRepository)
        {
            _mapper = mapper;
            _logEmailSendReadRepository = logEmailSendReadRepository;
            _logEmailSendBusinessRules = logEmailSendBusinessRules;
            _logEmailSendWriteRepository = logEmailSendWriteRepository;
        }

        public async Task<DeletedLogEmailSendResponse> Handle(DeleteLogEmailSendCommand request, CancellationToken cancellationToken)
        {
            X.LogEmailSend? logEmailSend = await _logEmailSendReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _logEmailSendBusinessRules.LogEmailSendShouldExistWhenSelected(logEmailSend);
            logEmailSend.DataState = Core.Enum.DataState.Deleted;

            _logEmailSendWriteRepository.Update(logEmailSend);
            await _logEmailSendWriteRepository.SaveAsync();

            return new()
            {
                Title = LogEmailSendsBusinessMessages.ProcessCompleted,
                Message = LogEmailSendsBusinessMessages.SuccessDeletedLogEmailSendMessage,
                IsValid = true
            };
        }
    }
}