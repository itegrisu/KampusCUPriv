using Application.Features.VehicleManagementFeatures.VehicleAlls.Constants;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Rules;
using Application.Repositories.VehicleManagementsRepos.VehicleAllRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Delete;

public class DeleteVehicleAllCommand : IRequest<DeletedVehicleAllResponse>
{
	public Guid Gid { get; set; }

    public class DeleteVehicleAllCommandHandler : IRequestHandler<DeleteVehicleAllCommand, DeletedVehicleAllResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleAllReadRepository _vehicleAllReadRepository;
        private readonly IVehicleAllWriteRepository _vehicleAllWriteRepository;
        private readonly VehicleAllBusinessRules _vehicleAllBusinessRules;

        public DeleteVehicleAllCommandHandler(IMapper mapper, IVehicleAllReadRepository vehicleAllReadRepository,
                                         VehicleAllBusinessRules vehicleAllBusinessRules, IVehicleAllWriteRepository vehicleAllWriteRepository)
        {
            _mapper = mapper;
            _vehicleAllReadRepository = vehicleAllReadRepository;
            _vehicleAllBusinessRules = vehicleAllBusinessRules;
            _vehicleAllWriteRepository = vehicleAllWriteRepository;
        }

        public async Task<DeletedVehicleAllResponse> Handle(DeleteVehicleAllCommand request, CancellationToken cancellationToken)
        {
            X.VehicleAll? vehicleAll = await _vehicleAllReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _vehicleAllBusinessRules.VehicleAllShouldExistWhenSelected(vehicleAll);
            vehicleAll.DataState = Core.Enum.DataState.Deleted;

            _vehicleAllWriteRepository.Update(vehicleAll);
            await _vehicleAllWriteRepository.SaveAsync();

            return new()
            {
                Title = VehicleAllsBusinessMessages.ProcessCompleted,
                Message = VehicleAllsBusinessMessages.SuccessDeletedVehicleAllMessage,
                IsValid = true
            };
        }
    }
}