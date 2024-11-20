using Application.Features.VehicleManagementFeatures.VehicleRequests.Constants;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleRequestRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Delete;

public class DeleteVehicleRequestCommand : IRequest<DeletedVehicleRequestResponse>
{
	public Guid Gid { get; set; }

    public class DeleteVehicleRequestCommandHandler : IRequestHandler<DeleteVehicleRequestCommand, DeletedVehicleRequestResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRequestReadRepository _vehicleRequestReadRepository;
        private readonly IVehicleRequestWriteRepository _vehicleRequestWriteRepository;
        private readonly VehicleRequestBusinessRules _vehicleRequestBusinessRules;

        public DeleteVehicleRequestCommandHandler(IMapper mapper, IVehicleRequestReadRepository vehicleRequestReadRepository,
                                         VehicleRequestBusinessRules vehicleRequestBusinessRules, IVehicleRequestWriteRepository vehicleRequestWriteRepository)
        {
            _mapper = mapper;
            _vehicleRequestReadRepository = vehicleRequestReadRepository;
            _vehicleRequestBusinessRules = vehicleRequestBusinessRules;
            _vehicleRequestWriteRepository = vehicleRequestWriteRepository;
        }

        public async Task<DeletedVehicleRequestResponse> Handle(DeleteVehicleRequestCommand request, CancellationToken cancellationToken)
        {
            X.VehicleRequest? vehicleRequest = await _vehicleRequestReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _vehicleRequestBusinessRules.VehicleRequestShouldExistWhenSelected(vehicleRequest);
            vehicleRequest.DataState = Core.Enum.DataState.Deleted;

            _vehicleRequestWriteRepository.Update(vehicleRequest);
            await _vehicleRequestWriteRepository.SaveAsync();

            return new()
            {
                Title = VehicleRequestsBusinessMessages.ProcessCompleted,
                Message = VehicleRequestsBusinessMessages.SuccessDeletedVehicleRequestMessage,
                IsValid = true
            };
        }
    }
}