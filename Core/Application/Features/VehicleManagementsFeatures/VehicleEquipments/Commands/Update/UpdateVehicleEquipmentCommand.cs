using Application.Features.VehicleManagementFeatures.VehicleEquipments.Constants;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleEquipmentRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Update;

public class UpdateVehicleEquipmentCommand : IRequest<UpdatedVehicleEquipmentResponse>
{
    public Guid Gid { get; set; }

	public Guid GidVehicleFK { get; set; }

public string EquipmentName { get; set; }
public string? DocumentFile { get; set; }
public string? Description { get; set; }



    public class UpdateVehicleEquipmentCommandHandler : IRequestHandler<UpdateVehicleEquipmentCommand, UpdatedVehicleEquipmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleEquipmentWriteRepository _vehicleEquipmentWriteRepository;
        private readonly IVehicleEquipmentReadRepository _vehicleEquipmentReadRepository;
        private readonly VehicleEquipmentBusinessRules _vehicleEquipmentBusinessRules;

        public UpdateVehicleEquipmentCommandHandler(IMapper mapper, IVehicleEquipmentWriteRepository vehicleEquipmentWriteRepository,
                                         VehicleEquipmentBusinessRules vehicleEquipmentBusinessRules, IVehicleEquipmentReadRepository vehicleEquipmentReadRepository)
        {
            _mapper = mapper;
            _vehicleEquipmentWriteRepository = vehicleEquipmentWriteRepository;
            _vehicleEquipmentBusinessRules = vehicleEquipmentBusinessRules;
            _vehicleEquipmentReadRepository = vehicleEquipmentReadRepository;
        }

        public async Task<UpdatedVehicleEquipmentResponse> Handle(UpdateVehicleEquipmentCommand request, CancellationToken cancellationToken)
        {
            X.VehicleEquipment? vehicleEquipment = await _vehicleEquipmentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.VehicleAllFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _vehicleEquipmentBusinessRules.VehicleEquipmentShouldExistWhenSelected(vehicleEquipment);
            vehicleEquipment = _mapper.Map(request, vehicleEquipment);

            _vehicleEquipmentWriteRepository.Update(vehicleEquipment!);
            await _vehicleEquipmentWriteRepository.SaveAsync();
            GetByGidVehicleEquipmentResponse obj = _mapper.Map<GetByGidVehicleEquipmentResponse>(vehicleEquipment);

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