using Application.Features.VehicleManagementFeatures.VehicleInsurances.Constants;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleInsuranceRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Delete;

public class DeleteVehicleInsuranceCommand : IRequest<DeletedVehicleInsuranceResponse>
{
	public Guid Gid { get; set; }

    public class DeleteVehicleInsuranceCommandHandler : IRequestHandler<DeleteVehicleInsuranceCommand, DeletedVehicleInsuranceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleInsuranceReadRepository _vehicleInsuranceReadRepository;
        private readonly IVehicleInsuranceWriteRepository _vehicleInsuranceWriteRepository;
        private readonly VehicleInsuranceBusinessRules _vehicleInsuranceBusinessRules;

        public DeleteVehicleInsuranceCommandHandler(IMapper mapper, IVehicleInsuranceReadRepository vehicleInsuranceReadRepository,
                                         VehicleInsuranceBusinessRules vehicleInsuranceBusinessRules, IVehicleInsuranceWriteRepository vehicleInsuranceWriteRepository)
        {
            _mapper = mapper;
            _vehicleInsuranceReadRepository = vehicleInsuranceReadRepository;
            _vehicleInsuranceBusinessRules = vehicleInsuranceBusinessRules;
            _vehicleInsuranceWriteRepository = vehicleInsuranceWriteRepository;
        }

        public async Task<DeletedVehicleInsuranceResponse> Handle(DeleteVehicleInsuranceCommand request, CancellationToken cancellationToken)
        {
            X.VehicleInsurance? vehicleInsurance = await _vehicleInsuranceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _vehicleInsuranceBusinessRules.VehicleInsuranceShouldExistWhenSelected(vehicleInsurance);
            vehicleInsurance.DataState = Core.Enum.DataState.Deleted;

            _vehicleInsuranceWriteRepository.Update(vehicleInsurance);
            await _vehicleInsuranceWriteRepository.SaveAsync();

            return new()
            {
                Title = VehicleInsurancesBusinessMessages.ProcessCompleted,
                Message = VehicleInsurancesBusinessMessages.SuccessDeletedVehicleInsuranceMessage,
                IsValid = true
            };
        }
    }
}