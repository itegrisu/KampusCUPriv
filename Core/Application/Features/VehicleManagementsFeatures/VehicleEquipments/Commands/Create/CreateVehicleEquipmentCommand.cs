using Application.Features.VehicleManagementFeatures.VehicleEquipments.Constants;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleEquipmentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Create;

public class CreateVehicleEquipmentCommand : IRequest<CreatedVehicleEquipmentResponse>
{
    public Guid GidVehicleFK { get; set; }

public string EquipmentName { get; set; }
public string? DocumentFile { get; set; }
public string? Description { get; set; }



    public class CreateVehicleEquipmentCommandHandler : IRequestHandler<CreateVehicleEquipmentCommand, CreatedVehicleEquipmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleEquipmentWriteRepository _vehicleEquipmentWriteRepository;
        private readonly IVehicleEquipmentReadRepository _vehicleEquipmentReadRepository;
        private readonly VehicleEquipmentBusinessRules _vehicleEquipmentBusinessRules;

        public CreateVehicleEquipmentCommandHandler(IMapper mapper, IVehicleEquipmentWriteRepository vehicleEquipmentWriteRepository,
                                         VehicleEquipmentBusinessRules vehicleEquipmentBusinessRules, IVehicleEquipmentReadRepository vehicleEquipmentReadRepository)
        {
            _mapper = mapper;
            _vehicleEquipmentWriteRepository = vehicleEquipmentWriteRepository;
            _vehicleEquipmentBusinessRules = vehicleEquipmentBusinessRules;
            _vehicleEquipmentReadRepository = vehicleEquipmentReadRepository;
        }

        public async Task<CreatedVehicleEquipmentResponse> Handle(CreateVehicleEquipmentCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _vehicleEquipmentReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.VehicleEquipment vehicleEquipment = _mapper.Map<X.VehicleEquipment>(request);
            //vehicleEquipment.RowNo = maxRowNo + 1;

            await _vehicleEquipmentWriteRepository.AddAsync(vehicleEquipment);
            await _vehicleEquipmentWriteRepository.SaveAsync();

			X.VehicleEquipment savedVehicleEquipment = await _vehicleEquipmentReadRepository.GetAsync(predicate: x => x.Gid == vehicleEquipment.Gid, include: x => x.Include(x => x.VehicleAllFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidVehicleEquipmentResponse obj = _mapper.Map<GetByGidVehicleEquipmentResponse>(savedVehicleEquipment);
            return new()
            {           
                Title = VehicleEquipmentsBusinessMessages.ProcessCompleted,
                Message = VehicleEquipmentsBusinessMessages.SuccessCreatedVehicleEquipmentMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}