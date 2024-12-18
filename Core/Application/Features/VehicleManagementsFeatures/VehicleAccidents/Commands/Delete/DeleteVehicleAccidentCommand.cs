using Application.Features.VehicleManagementFeatures.VehicleAccidents.Constants;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Rules;
using AutoMapper;
using X = Domain.Entities.VehicleManagements;
using MediatR;
using Application.Repositories.VehicleManagementsRepos.VehicleAccidentRepo;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Delete;

public class DeleteVehicleAccidentCommand : IRequest<DeletedVehicleAccidentResponse>
{
	public Guid Gid { get; set; }

    public class DeleteVehicleAccidentCommandHandler : IRequestHandler<DeleteVehicleAccidentCommand, DeletedVehicleAccidentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleAccidentReadRepository _vehicleAccidentReadRepository;
        private readonly IVehicleAccidentWriteRepository _vehicleAccidentWriteRepository;
        private readonly VehicleAccidentBusinessRules _vehicleAccidentBusinessRules;

        public DeleteVehicleAccidentCommandHandler(IMapper mapper, IVehicleAccidentReadRepository vehicleAccidentReadRepository,
                                         VehicleAccidentBusinessRules vehicleAccidentBusinessRules, IVehicleAccidentWriteRepository vehicleAccidentWriteRepository)
        {
            _mapper = mapper;
            _vehicleAccidentReadRepository = vehicleAccidentReadRepository;
            _vehicleAccidentBusinessRules = vehicleAccidentBusinessRules;
            _vehicleAccidentWriteRepository = vehicleAccidentWriteRepository;
        }

        public async Task<DeletedVehicleAccidentResponse> Handle(DeleteVehicleAccidentCommand request, CancellationToken cancellationToken)
        {
            X.VehicleAccident? vehicleAccident = await _vehicleAccidentReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _vehicleAccidentBusinessRules.VehicleAccidentShouldExistWhenSelected(vehicleAccident);
            vehicleAccident.DataState = Core.Enum.DataState.Deleted;

            _vehicleAccidentWriteRepository.Update(vehicleAccident);
            await _vehicleAccidentWriteRepository.SaveAsync();

            return new()
            {
                Title = VehicleAccidentsBusinessMessages.ProcessCompleted,
                Message = VehicleAccidentsBusinessMessages.SuccessDeletedVehicleAccidentMessage,
                IsValid = true
            };
        }
    }
}