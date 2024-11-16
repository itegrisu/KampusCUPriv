using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Constants;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleMaintenanceRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Delete;

public class DeleteVehicleMaintenanceCommand : IRequest<DeletedVehicleMaintenanceResponse>
{
	public Guid Gid { get; set; }

    public class DeleteVehicleMaintenanceCommandHandler : IRequestHandler<DeleteVehicleMaintenanceCommand, DeletedVehicleMaintenanceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleMaintenanceReadRepository _vehicleMaintenanceReadRepository;
        private readonly IVehicleMaintenanceWriteRepository _vehicleMaintenanceWriteRepository;
        private readonly VehicleMaintenanceBusinessRules _vehicleMaintenanceBusinessRules;

        public DeleteVehicleMaintenanceCommandHandler(IMapper mapper, IVehicleMaintenanceReadRepository vehicleMaintenanceReadRepository,
                                         VehicleMaintenanceBusinessRules vehicleMaintenanceBusinessRules, IVehicleMaintenanceWriteRepository vehicleMaintenanceWriteRepository)
        {
            _mapper = mapper;
            _vehicleMaintenanceReadRepository = vehicleMaintenanceReadRepository;
            _vehicleMaintenanceBusinessRules = vehicleMaintenanceBusinessRules;
            _vehicleMaintenanceWriteRepository = vehicleMaintenanceWriteRepository;
        }

        public async Task<DeletedVehicleMaintenanceResponse> Handle(DeleteVehicleMaintenanceCommand request, CancellationToken cancellationToken)
        {
            X.VehicleMaintenance? vehicleMaintenance = await _vehicleMaintenanceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
            await _vehicleMaintenanceBusinessRules.VehicleMaintenanceShouldExistWhenSelected(vehicleMaintenance);
            vehicleMaintenance.DataState = Core.Enum.DataState.Deleted;

            _vehicleMaintenanceWriteRepository.Update(vehicleMaintenance);
            await _vehicleMaintenanceWriteRepository.SaveAsync();

            return new()
            {
                Title = VehicleMaintenancesBusinessMessages.ProcessCompleted,
                Message = VehicleMaintenancesBusinessMessages.SuccessDeletedVehicleMaintenanceMessage,
                IsValid = true
            };
        }
    }
}