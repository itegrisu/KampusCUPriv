using Application.Features.TransportationManagementFeatures.TransportationGroups.Constants;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Rules;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Delete;

public class DeleteTransportationGroupCommand : IRequest<DeletedTransportationGroupResponse>
{
	public Guid Gid { get; set; }

    public class DeleteTransportationGroupCommandHandler : IRequestHandler<DeleteTransportationGroupCommand, DeletedTransportationGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
        private readonly ITransportationGroupWriteRepository _transportationGroupWriteRepository;
        private readonly TransportationGroupBusinessRules _transportationGroupBusinessRules;

        public DeleteTransportationGroupCommandHandler(IMapper mapper, ITransportationGroupReadRepository transportationGroupReadRepository,
                                         TransportationGroupBusinessRules transportationGroupBusinessRules, ITransportationGroupWriteRepository transportationGroupWriteRepository)
        {
            _mapper = mapper;
            _transportationGroupReadRepository = transportationGroupReadRepository;
            _transportationGroupBusinessRules = transportationGroupBusinessRules;
            _transportationGroupWriteRepository = transportationGroupWriteRepository;
        }

        public async Task<DeletedTransportationGroupResponse> Handle(DeleteTransportationGroupCommand request, CancellationToken cancellationToken)
        {
            X.TransportationGroup? transportationGroup = await _transportationGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _transportationGroupBusinessRules.TransportationGroupShouldExistWhenSelected(transportationGroup);
            transportationGroup.DataState = Core.Enum.DataState.Deleted;

            _transportationGroupWriteRepository.Update(transportationGroup);
            await _transportationGroupWriteRepository.SaveAsync();

            return new()
            {
                Title = TransportationGroupsBusinessMessages.ProcessCompleted,
                Message = TransportationGroupsBusinessMessages.SuccessDeletedTransportationGroupMessage,
                IsValid = true
            };
        }
    }
}