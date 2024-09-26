using Application.Features.SupportManagementFeatures.SupportRequests.Constants;
using Application.Features.SupportManagementFeatures.SupportRequests.Rules;
using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using AutoMapper;
using X = Domain.Entities.SupportManagements;
using MediatR;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Commands.Delete;

public class DeleteSupportRequestCommand : IRequest<DeletedSupportRequestResponse>
{
	public Guid Gid { get; set; }

    public class DeleteSupportRequestCommandHandler : IRequestHandler<DeleteSupportRequestCommand, DeletedSupportRequestResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportRequestReadRepository _supportRequestReadRepository;
        private readonly ISupportRequestWriteRepository _supportRequestWriteRepository;
        private readonly SupportRequestBusinessRules _supportRequestBusinessRules;

        public DeleteSupportRequestCommandHandler(IMapper mapper, ISupportRequestReadRepository supportRequestReadRepository,
                                         SupportRequestBusinessRules supportRequestBusinessRules, ISupportRequestWriteRepository supportRequestWriteRepository)
        {
            _mapper = mapper;
            _supportRequestReadRepository = supportRequestReadRepository;
            _supportRequestBusinessRules = supportRequestBusinessRules;
            _supportRequestWriteRepository = supportRequestWriteRepository;
        }

        public async Task<DeletedSupportRequestResponse> Handle(DeleteSupportRequestCommand request, CancellationToken cancellationToken)
        {
            X.SupportRequest? supportRequest = await _supportRequestReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _supportRequestBusinessRules.SupportRequestShouldExistWhenSelected(supportRequest);
            supportRequest.DataState = Core.Enum.DataState.Deleted;

            _supportRequestWriteRepository.Update(supportRequest);
            await _supportRequestWriteRepository.SaveAsync();

            return new()
            {
                Title = SupportRequestsBusinessMessages.ProcessCompleted,
                Message = SupportRequestsBusinessMessages.SuccessDeletedSupportRequestMessage,
                IsValid = true
            };
        }
    }
}