using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Constants;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Rules;
using Application.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Delete;

public class DeleteLogUserPageVisitActionCommand : IRequest<DeletedLogUserPageVisitActionResponse>
{
	public Guid Gid { get; set; }

    public class DeleteLogUserPageVisitActionCommandHandler : IRequestHandler<DeleteLogUserPageVisitActionCommand, DeletedLogUserPageVisitActionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogUserPageVisitActionReadRepository _logUserPageVisitActionReadRepository;
        private readonly ILogUserPageVisitActionWriteRepository _logUserPageVisitActionWriteRepository;
        private readonly LogUserPageVisitActionBusinessRules _logUserPageVisitActionBusinessRules;

        public DeleteLogUserPageVisitActionCommandHandler(IMapper mapper, ILogUserPageVisitActionReadRepository logUserPageVisitActionReadRepository,
                                         LogUserPageVisitActionBusinessRules logUserPageVisitActionBusinessRules, ILogUserPageVisitActionWriteRepository logUserPageVisitActionWriteRepository)
        {
            _mapper = mapper;
            _logUserPageVisitActionReadRepository = logUserPageVisitActionReadRepository;
            _logUserPageVisitActionBusinessRules = logUserPageVisitActionBusinessRules;
            _logUserPageVisitActionWriteRepository = logUserPageVisitActionWriteRepository;
        }

        public async Task<DeletedLogUserPageVisitActionResponse> Handle(DeleteLogUserPageVisitActionCommand request, CancellationToken cancellationToken)
        {
            X.LogUserPageVisitAction? logUserPageVisitAction = await _logUserPageVisitActionReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _logUserPageVisitActionBusinessRules.LogUserPageVisitActionShouldExistWhenSelected(logUserPageVisitAction);
            logUserPageVisitAction.DataState = Core.Enum.DataState.Deleted;

            _logUserPageVisitActionWriteRepository.Update(logUserPageVisitAction);
            await _logUserPageVisitActionWriteRepository.SaveAsync();

            return new()
            {
                Title = LogUserPageVisitActionsBusinessMessages.ProcessCompleted,
                Message = LogUserPageVisitActionsBusinessMessages.SuccessDeletedLogUserPageVisitActionMessage,
                IsValid = true
            };
        }
    }
}