using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Constants;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleMaintenanceRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Update;

public class UpdateVehicleMaintenanceCommand : IRequest<UpdatedVehicleMaintenanceResponse>
{
    public Guid Gid { get; set; }

	public Guid GidVehicleFK { get; set; }

public DateTime MaintenanceDate { get; set; }
public string? ResponsiblePerson { get; set; }
public int MaintenanceFee { get; set; }
public string? DocumentFile { get; set; }
public string? Description { get; set; }
public int? MaintenanceScore { get; set; }



    public class UpdateVehicleMaintenanceCommandHandler : IRequestHandler<UpdateVehicleMaintenanceCommand, UpdatedVehicleMaintenanceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleMaintenanceWriteRepository _vehicleMaintenanceWriteRepository;
        private readonly IVehicleMaintenanceReadRepository _vehicleMaintenanceReadRepository;
        private readonly VehicleMaintenanceBusinessRules _vehicleMaintenanceBusinessRules;

        public UpdateVehicleMaintenanceCommandHandler(IMapper mapper, IVehicleMaintenanceWriteRepository vehicleMaintenanceWriteRepository,
                                         VehicleMaintenanceBusinessRules vehicleMaintenanceBusinessRules, IVehicleMaintenanceReadRepository vehicleMaintenanceReadRepository)
        {
            _mapper = mapper;
            _vehicleMaintenanceWriteRepository = vehicleMaintenanceWriteRepository;
            _vehicleMaintenanceBusinessRules = vehicleMaintenanceBusinessRules;
            _vehicleMaintenanceReadRepository = vehicleMaintenanceReadRepository;
        }

        public async Task<UpdatedVehicleMaintenanceResponse> Handle(UpdateVehicleMaintenanceCommand request, CancellationToken cancellationToken)
        {
            X.VehicleMaintenance? vehicleMaintenance = await _vehicleMaintenanceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _vehicleMaintenanceBusinessRules.VehicleMaintenanceShouldExistWhenSelected(vehicleMaintenance);
            vehicleMaintenance = _mapper.Map(request, vehicleMaintenance);

            _vehicleMaintenanceWriteRepository.Update(vehicleMaintenance!);
            await _vehicleMaintenanceWriteRepository.SaveAsync();
            GetByGidVehicleMaintenanceResponse obj = _mapper.Map<GetByGidVehicleMaintenanceResponse>(vehicleMaintenance);

            return new()
            {
                Title = VehicleMaintenancesBusinessMessages.ProcessCompleted,
                Message = VehicleMaintenancesBusinessMessages.SuccessCreatedVehicleMaintenanceMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}