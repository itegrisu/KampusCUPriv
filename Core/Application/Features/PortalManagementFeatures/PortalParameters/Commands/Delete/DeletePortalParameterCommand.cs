using Application.Features.PortalManagementFeatures.PortalParameters.Constants;
using Application.Features.PortalManagementFeatures.PortalParameters.Rules;
using Application.Repositories.PortalManagementRepos.PortalParameterRepo;
using AutoMapper;
using X = Domain.Entities.PortalManagements;
using MediatR;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Commands.Delete;

public class DeletePortalParameterCommand : IRequest<DeletedPortalParameterResponse>
{
	public Guid Gid { get; set; }

    public class DeletePortalParameterCommandHandler : IRequestHandler<DeletePortalParameterCommand, DeletedPortalParameterResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPortalParameterReadRepository _portalParameterReadRepository;
        private readonly IPortalParameterWriteRepository _portalParameterWriteRepository;
        private readonly PortalParameterBusinessRules _portalParameterBusinessRules;

        public DeletePortalParameterCommandHandler(IMapper mapper, IPortalParameterReadRepository portalParameterReadRepository,
                                         PortalParameterBusinessRules portalParameterBusinessRules, IPortalParameterWriteRepository portalParameterWriteRepository)
        {
            _mapper = mapper;
            _portalParameterReadRepository = portalParameterReadRepository;
            _portalParameterBusinessRules = portalParameterBusinessRules;
            _portalParameterWriteRepository = portalParameterWriteRepository;
        }

        public async Task<DeletedPortalParameterResponse> Handle(DeletePortalParameterCommand request, CancellationToken cancellationToken)
        {
            X.PortalParameter? portalParameter = await _portalParameterReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _portalParameterBusinessRules.PortalParameterShouldExistWhenSelected(portalParameter);
            portalParameter.DataState = Core.Enum.DataState.Deleted;

            _portalParameterWriteRepository.Update(portalParameter);
            await _portalParameterWriteRepository.SaveAsync();

            return new()
            {
                Title = PortalParametersBusinessMessages.ProcessCompleted,
                Message = PortalParametersBusinessMessages.SuccessDeletedPortalParameterMessage,
                IsValid = true
            };
        }
    }
}