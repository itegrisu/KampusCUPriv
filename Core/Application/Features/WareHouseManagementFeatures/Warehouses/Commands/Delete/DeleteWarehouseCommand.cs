using Application.Features.WarehouseManagementFeatures.Warehouses.Constants;
using Application.Features.WarehouseManagementFeatures.Warehouses.Rules;
using Application.Repositories.WarehouseManagementRepos.WarehouseRepo;
using AutoMapper;
using X = Domain.Entities.WarehouseManagements;
using MediatR;

namespace Application.Features.WarehouseManagementFeatures.Warehouses.Commands.Delete;

public class DeleteWarehouseCommand : IRequest<DeletedWarehouseResponse>
{
	public Guid Gid { get; set; }

    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, DeletedWarehouseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseReadRepository _warehouseReadRepository;
        private readonly IWarehouseWriteRepository _warehouseWriteRepository;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;

        public DeleteWarehouseCommandHandler(IMapper mapper, IWarehouseReadRepository warehouseReadRepository,
                                         WarehouseBusinessRules warehouseBusinessRules, IWarehouseWriteRepository warehouseWriteRepository)
        {
            _mapper = mapper;
            _warehouseReadRepository = warehouseReadRepository;
            _warehouseBusinessRules = warehouseBusinessRules;
            _warehouseWriteRepository = warehouseWriteRepository;
        }

        public async Task<DeletedWarehouseResponse> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            X.Warehouse? warehouse = await _warehouseReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _warehouseBusinessRules.WarehouseShouldExistWhenSelected(warehouse);
            warehouse.DataState = Core.Enum.DataState.Deleted;

            _warehouseWriteRepository.Update(warehouse);
            await _warehouseWriteRepository.SaveAsync();

            return new()
            {
                Title = WarehousesBusinessMessages.ProcessCompleted,
                Message = WarehousesBusinessMessages.SuccessDeletedWarehouseMessage,
                IsValid = true
            };
        }
    }
}