using Application.Features.LogManagementFeatures.LogUserPageVisits.Constants;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Rules;
using Application.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Delete;

public class DeleteLogUserPageVisitCommand : IRequest<DeletedLogUserPageVisitResponse>
{
	public Guid Gid { get; set; }

    public class DeleteLogUserPageVisitCommandHandler : IRequestHandler<DeleteLogUserPageVisitCommand, DeletedLogUserPageVisitResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogUserPageVisitReadRepository _logUserPageVisitReadRepository;
        private readonly ILogUserPageVisitWriteRepository _logUserPageVisitWriteRepository;
        private readonly LogUserPageVisitBusinessRules _logUserPageVisitBusinessRules;

        public DeleteLogUserPageVisitCommandHandler(IMapper mapper, ILogUserPageVisitReadRepository logUserPageVisitReadRepository,
                                         LogUserPageVisitBusinessRules logUserPageVisitBusinessRules, ILogUserPageVisitWriteRepository logUserPageVisitWriteRepository)
        {
            _mapper = mapper;
            _logUserPageVisitReadRepository = logUserPageVisitReadRepository;
            _logUserPageVisitBusinessRules = logUserPageVisitBusinessRules;
            _logUserPageVisitWriteRepository = logUserPageVisitWriteRepository;
        }

        public async Task<DeletedLogUserPageVisitResponse> Handle(DeleteLogUserPageVisitCommand request, CancellationToken cancellationToken)
        {
            X.LogUserPageVisit? logUserPageVisit = await _logUserPageVisitReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _logUserPageVisitBusinessRules.LogUserPageVisitShouldExistWhenSelected(logUserPageVisit);
            logUserPageVisit.DataState = Core.Enum.DataState.Deleted;

            _logUserPageVisitWriteRepository.Update(logUserPageVisit);
            await _logUserPageVisitWriteRepository.SaveAsync();

            return new()
            {
                Title = LogUserPageVisitsBusinessMessages.ProcessCompleted,
                Message = LogUserPageVisitsBusinessMessages.SuccessDeletedLogUserPageVisitMessage,
                IsValid = true
            };
        }
    }
}