using Application.Features.PortalManagementFeatures.PortalTexts.Constants;
using Application.Features.PortalManagementFeatures.PortalTexts.Rules;
using Application.Repositories.PortalManagementRepos.PortalTextRepo;
using AutoMapper;
using X = Domain.Entities.PortalManagements;
using MediatR;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Commands.Delete;

public class DeletePortalTextCommand : IRequest<DeletedPortalTextResponse>
{
	public Guid Gid { get; set; }

    public class DeletePortalTextCommandHandler : IRequestHandler<DeletePortalTextCommand, DeletedPortalTextResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPortalTextReadRepository _portalTextReadRepository;
        private readonly IPortalTextWriteRepository _portalTextWriteRepository;
        private readonly PortalTextBusinessRules _portalTextBusinessRules;

        public DeletePortalTextCommandHandler(IMapper mapper, IPortalTextReadRepository portalTextReadRepository,
                                         PortalTextBusinessRules portalTextBusinessRules, IPortalTextWriteRepository portalTextWriteRepository)
        {
            _mapper = mapper;
            _portalTextReadRepository = portalTextReadRepository;
            _portalTextBusinessRules = portalTextBusinessRules;
            _portalTextWriteRepository = portalTextWriteRepository;
        }

        public async Task<DeletedPortalTextResponse> Handle(DeletePortalTextCommand request, CancellationToken cancellationToken)
        {
            X.PortalText? portalText = await _portalTextReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _portalTextBusinessRules.PortalTextShouldExistWhenSelected(portalText);
            portalText.DataState = Core.Enum.DataState.Deleted;

            _portalTextWriteRepository.Update(portalText);
            await _portalTextWriteRepository.SaveAsync();

            return new()
            {
                Title = PortalTextsBusinessMessages.ProcessCompleted,
                Message = PortalTextsBusinessMessages.SuccessDeletedPortalTextMessage,
                IsValid = true
            };
        }
    }
}