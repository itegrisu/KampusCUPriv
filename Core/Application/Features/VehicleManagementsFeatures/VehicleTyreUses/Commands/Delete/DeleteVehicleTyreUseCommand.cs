using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Constants;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleTyreUseRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Delete;

public class DeleteVehicleTyreUseCommand : IRequest<DeletedVehicleTyreUseResponse>
{
	public Guid Gid { get; set; }

    public class DeleteVehicleTyreUseCommandHandler : IRequestHandler<DeleteVehicleTyreUseCommand, DeletedVehicleTyreUseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleTyreUseReadRepository _vehicleTyreUseReadRepository;
        private readonly IVehicleTyreUseWriteRepository _vehicleTyreUseWriteRepository;
        private readonly VehicleTyreUseBusinessRules _vehicleTyreUseBusinessRules;

        public DeleteVehicleTyreUseCommandHandler(IMapper mapper, IVehicleTyreUseReadRepository vehicleTyreUseReadRepository,
                                         VehicleTyreUseBusinessRules vehicleTyreUseBusinessRules, IVehicleTyreUseWriteRepository vehicleTyreUseWriteRepository)
        {
            _mapper = mapper;
            _vehicleTyreUseReadRepository = vehicleTyreUseReadRepository;
            _vehicleTyreUseBusinessRules = vehicleTyreUseBusinessRules;
            _vehicleTyreUseWriteRepository = vehicleTyreUseWriteRepository;
        }

        public async Task<DeletedVehicleTyreUseResponse> Handle(DeleteVehicleTyreUseCommand request, CancellationToken cancellationToken)
        {
            X.VehicleTyreUse? vehicleTyreUse = await _vehicleTyreUseReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _vehicleTyreUseBusinessRules.VehicleTyreUseShouldExistWhenSelected(vehicleTyreUse);
            vehicleTyreUse.DataState = Core.Enum.DataState.Deleted;

            _vehicleTyreUseWriteRepository.Update(vehicleTyreUse);
            await _vehicleTyreUseWriteRepository.SaveAsync();

            return new()
            {
                Title = VehicleTyreUsesBusinessMessages.ProcessCompleted,
                Message = VehicleTyreUsesBusinessMessages.SuccessDeletedVehicleTyreUseMessage,
                IsValid = true
            };
        }
    }
}