using Application.Features.VehicleManagementFeatures.VehicleTransactions.Constants;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Rules;
using AutoMapper;
using X = Domain.Entities.VehicleManagements;
using MediatR;
using Domain.Enums;
using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Delete;

public class DeleteVehicleTransactionCommand : IRequest<DeletedVehicleTransactionResponse>
{
	public Guid Gid { get; set; }

    public class DeleteVehicleTransactionCommandHandler : IRequestHandler<DeleteVehicleTransactionCommand, DeletedVehicleTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleTransactionReadRepository _vehicleTransactionReadRepository;
        private readonly IVehicleTransactionWriteRepository _vehicleTransactionWriteRepository;
        private readonly VehicleTransactionBusinessRules _vehicleTransactionBusinessRules;

        public DeleteVehicleTransactionCommandHandler(IMapper mapper, IVehicleTransactionReadRepository vehicleTransactionReadRepository,
                                         VehicleTransactionBusinessRules vehicleTransactionBusinessRules, IVehicleTransactionWriteRepository vehicleTransactionWriteRepository)
        {
            _mapper = mapper;
            _vehicleTransactionReadRepository = vehicleTransactionReadRepository;
            _vehicleTransactionBusinessRules = vehicleTransactionBusinessRules;
            _vehicleTransactionWriteRepository = vehicleTransactionWriteRepository;
        }

        public async Task<DeletedVehicleTransactionResponse> Handle(DeleteVehicleTransactionCommand request, CancellationToken cancellationToken)
        {
            X.VehicleTransaction? vehicleTransaction = await _vehicleTransactionReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _vehicleTransactionBusinessRules.VehicleTransactionShouldExistWhenSelected(vehicleTransaction);
            vehicleTransaction.DataState = Core.Enum.DataState.Deleted;

            _vehicleTransactionWriteRepository.Update(vehicleTransaction);
            await _vehicleTransactionWriteRepository.SaveAsync();

            return new()
            {
                Title = VehicleTransactionsBusinessMessages.ProcessCompleted,
                Message = VehicleTransactionsBusinessMessages.SuccessDeletedVehicleTransactionMessage,
                IsValid = true
            };
        }
    }
}