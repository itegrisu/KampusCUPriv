using Application.Features.VehicleManagementFeatures.VehicleInspections.Constants;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleInspectionRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Delete;

public class DeleteVehicleInspectionCommand : IRequest<DeletedVehicleInspectionResponse>
{
	public Guid Gid { get; set; }

    public class DeleteVehicleInspectionCommandHandler : IRequestHandler<DeleteVehicleInspectionCommand, DeletedVehicleInspectionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleInspectionReadRepository _vehicleInspectionReadRepository;
        private readonly IVehicleInspectionWriteRepository _vehicleInspectionWriteRepository;
        private readonly VehicleInspectionBusinessRules _vehicleInspectionBusinessRules;

        public DeleteVehicleInspectionCommandHandler(IMapper mapper, IVehicleInspectionReadRepository vehicleInspectionReadRepository,
                                         VehicleInspectionBusinessRules vehicleInspectionBusinessRules, IVehicleInspectionWriteRepository vehicleInspectionWriteRepository)
        {
            _mapper = mapper;
            _vehicleInspectionReadRepository = vehicleInspectionReadRepository;
            _vehicleInspectionBusinessRules = vehicleInspectionBusinessRules;
            _vehicleInspectionWriteRepository = vehicleInspectionWriteRepository;
        }

        public async Task<DeletedVehicleInspectionResponse> Handle(DeleteVehicleInspectionCommand request, CancellationToken cancellationToken)
        {
            X.VehicleInspection? vehicleInspection = await _vehicleInspectionReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _vehicleInspectionBusinessRules.VehicleInspectionShouldExistWhenSelected(vehicleInspection);
            vehicleInspection.DataState = Core.Enum.DataState.Deleted;

            _vehicleInspectionWriteRepository.Update(vehicleInspection);
            await _vehicleInspectionWriteRepository.SaveAsync();

            return new()
            {
                Title = VehicleInspectionsBusinessMessages.ProcessCompleted,
                Message = VehicleInspectionsBusinessMessages.SuccessDeletedVehicleInspectionMessage,
                IsValid = true
            };
        }
    }
}