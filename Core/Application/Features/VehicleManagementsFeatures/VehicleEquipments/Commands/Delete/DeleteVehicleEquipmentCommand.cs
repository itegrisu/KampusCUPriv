using Application.Features.VehicleManagementFeatures.VehicleEquipments.Constants;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleEquipmentRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Delete;

public class DeleteVehicleEquipmentCommand : IRequest<DeletedVehicleEquipmentResponse>
{
	public Guid Gid { get; set; }

    public class DeleteVehicleEquipmentCommandHandler : IRequestHandler<DeleteVehicleEquipmentCommand, DeletedVehicleEquipmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleEquipmentReadRepository _vehicleEquipmentReadRepository;
        private readonly IVehicleEquipmentWriteRepository _vehicleEquipmentWriteRepository;
        private readonly VehicleEquipmentBusinessRules _vehicleEquipmentBusinessRules;

        public DeleteVehicleEquipmentCommandHandler(IMapper mapper, IVehicleEquipmentReadRepository vehicleEquipmentReadRepository,
                                         VehicleEquipmentBusinessRules vehicleEquipmentBusinessRules, IVehicleEquipmentWriteRepository vehicleEquipmentWriteRepository)
        {
            _mapper = mapper;
            _vehicleEquipmentReadRepository = vehicleEquipmentReadRepository;
            _vehicleEquipmentBusinessRules = vehicleEquipmentBusinessRules;
            _vehicleEquipmentWriteRepository = vehicleEquipmentWriteRepository;
        }

        public async Task<DeletedVehicleEquipmentResponse> Handle(DeleteVehicleEquipmentCommand request, CancellationToken cancellationToken)
        {
            X.VehicleEquipment? vehicleEquipment = await _vehicleEquipmentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _vehicleEquipmentBusinessRules.VehicleEquipmentShouldExistWhenSelected(vehicleEquipment);
            vehicleEquipment.DataState = Core.Enum.DataState.Deleted;

            _vehicleEquipmentWriteRepository.Update(vehicleEquipment);
            await _vehicleEquipmentWriteRepository.SaveAsync();

            return new()
            {
                Title = VehicleEquipmentsBusinessMessages.ProcessCompleted,
                Message = VehicleEquipmentsBusinessMessages.SuccessDeletedVehicleEquipmentMessage,
                IsValid = true
            };
        }
    }
}