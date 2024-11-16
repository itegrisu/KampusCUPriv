using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Constants;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleMaintenanceRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Create;

public class CreateVehicleMaintenanceCommand : IRequest<CreatedVehicleMaintenanceResponse>
{
    public Guid GidVehicleFK { get; set; }

public DateTime MaintenanceDate { get; set; }
public string? ResponsiblePerson { get; set; }
public int MaintenanceFee { get; set; }
public string? DocumentFile { get; set; }
public string? Description { get; set; }
public int? MaintenanceScore { get; set; }



    public class CreateVehicleMaintenanceCommandHandler : IRequestHandler<CreateVehicleMaintenanceCommand, CreatedVehicleMaintenanceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleMaintenanceWriteRepository _vehicleMaintenanceWriteRepository;
        private readonly IVehicleMaintenanceReadRepository _vehicleMaintenanceReadRepository;
        private readonly VehicleMaintenanceBusinessRules _vehicleMaintenanceBusinessRules;

        public CreateVehicleMaintenanceCommandHandler(IMapper mapper, IVehicleMaintenanceWriteRepository vehicleMaintenanceWriteRepository,
                                         VehicleMaintenanceBusinessRules vehicleMaintenanceBusinessRules, IVehicleMaintenanceReadRepository vehicleMaintenanceReadRepository)
        {
            _mapper = mapper;
            _vehicleMaintenanceWriteRepository = vehicleMaintenanceWriteRepository;
            _vehicleMaintenanceBusinessRules = vehicleMaintenanceBusinessRules;
            _vehicleMaintenanceReadRepository = vehicleMaintenanceReadRepository;
        }

        public async Task<CreatedVehicleMaintenanceResponse> Handle(CreateVehicleMaintenanceCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _vehicleMaintenanceReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.VehicleMaintenance vehicleMaintenance = _mapper.Map<X.VehicleMaintenance>(request);
            //vehicleMaintenance.RowNo = maxRowNo + 1;

            await _vehicleMaintenanceWriteRepository.AddAsync(vehicleMaintenance);
            await _vehicleMaintenanceWriteRepository.SaveAsync();

			X.VehicleMaintenance savedVehicleMaintenance = await _vehicleMaintenanceReadRepository.GetAsync(predicate: x => x.Gid == vehicleMaintenance.Gid, include: x => x.Include(x => x.VehicleAllFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidVehicleMaintenanceResponse obj = _mapper.Map<GetByGidVehicleMaintenanceResponse>(savedVehicleMaintenance);
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