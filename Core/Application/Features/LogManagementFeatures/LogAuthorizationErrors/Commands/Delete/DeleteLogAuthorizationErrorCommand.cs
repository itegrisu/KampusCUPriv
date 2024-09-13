using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Constants;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Rules;
using Application.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Delete;

public class DeleteLogAuthorizationErrorCommand : IRequest<DeletedLogAuthorizationErrorResponse>
{
	public Guid Gid { get; set; }

    public class DeleteLogAuthorizationErrorCommandHandler : IRequestHandler<DeleteLogAuthorizationErrorCommand, DeletedLogAuthorizationErrorResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogAuthorizationErrorReadRepository _logAuthorizationErrorReadRepository;
        private readonly ILogAuthorizationErrorWriteRepository _logAuthorizationErrorWriteRepository;
        private readonly LogAuthorizationErrorBusinessRules _logAuthorizationErrorBusinessRules;

        public DeleteLogAuthorizationErrorCommandHandler(IMapper mapper, ILogAuthorizationErrorReadRepository logAuthorizationErrorReadRepository,
                                         LogAuthorizationErrorBusinessRules logAuthorizationErrorBusinessRules, ILogAuthorizationErrorWriteRepository logAuthorizationErrorWriteRepository)
        {
            _mapper = mapper;
            _logAuthorizationErrorReadRepository = logAuthorizationErrorReadRepository;
            _logAuthorizationErrorBusinessRules = logAuthorizationErrorBusinessRules;
            _logAuthorizationErrorWriteRepository = logAuthorizationErrorWriteRepository;
        }

        public async Task<DeletedLogAuthorizationErrorResponse> Handle(DeleteLogAuthorizationErrorCommand request, CancellationToken cancellationToken)
        {
            X.LogAuthorizationError? logAuthorizationError = await _logAuthorizationErrorReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _logAuthorizationErrorBusinessRules.LogAuthorizationErrorShouldExistWhenSelected(logAuthorizationError);
            logAuthorizationError.DataState = Core.Enum.DataState.Deleted;

            _logAuthorizationErrorWriteRepository.Update(logAuthorizationError);
            await _logAuthorizationErrorWriteRepository.SaveAsync();

            return new()
            {
                Title = LogAuthorizationErrorsBusinessMessages.ProcessCompleted,
                Message = LogAuthorizationErrorsBusinessMessages.SuccessDeletedLogAuthorizationErrorMessage,
                IsValid = true
            };
        }
    }
}