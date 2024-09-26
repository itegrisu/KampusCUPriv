using Application.Features.SupportManagementFeatures.SupportMessageDetails.Constants;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Rules;
using Application.Repositories.SupportManagementRepos.SupportMessageDetailRepo;
using AutoMapper;
using X = Domain.Entities.SupportManagements;
using MediatR;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Delete;

public class DeleteSupportMessageDetailCommand : IRequest<DeletedSupportMessageDetailResponse>
{
	public Guid Gid { get; set; }

    public class DeleteSupportMessageDetailCommandHandler : IRequestHandler<DeleteSupportMessageDetailCommand, DeletedSupportMessageDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportMessageDetailReadRepository _supportMessageDetailReadRepository;
        private readonly ISupportMessageDetailWriteRepository _supportMessageDetailWriteRepository;
        private readonly SupportMessageDetailBusinessRules _supportMessageDetailBusinessRules;

        public DeleteSupportMessageDetailCommandHandler(IMapper mapper, ISupportMessageDetailReadRepository supportMessageDetailReadRepository,
                                         SupportMessageDetailBusinessRules supportMessageDetailBusinessRules, ISupportMessageDetailWriteRepository supportMessageDetailWriteRepository)
        {
            _mapper = mapper;
            _supportMessageDetailReadRepository = supportMessageDetailReadRepository;
            _supportMessageDetailBusinessRules = supportMessageDetailBusinessRules;
            _supportMessageDetailWriteRepository = supportMessageDetailWriteRepository;
        }

        public async Task<DeletedSupportMessageDetailResponse> Handle(DeleteSupportMessageDetailCommand request, CancellationToken cancellationToken)
        {
            X.SupportMessageDetail? supportMessageDetail = await _supportMessageDetailReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _supportMessageDetailBusinessRules.SupportMessageDetailShouldExistWhenSelected(supportMessageDetail);
            supportMessageDetail.DataState = Core.Enum.DataState.Deleted;

            _supportMessageDetailWriteRepository.Update(supportMessageDetail);
            await _supportMessageDetailWriteRepository.SaveAsync();

            return new()
            {
                Title = SupportMessageDetailsBusinessMessages.ProcessCompleted,
                Message = SupportMessageDetailsBusinessMessages.SuccessDeletedSupportMessageDetailMessage,
                IsValid = true
            };
        }
    }
}