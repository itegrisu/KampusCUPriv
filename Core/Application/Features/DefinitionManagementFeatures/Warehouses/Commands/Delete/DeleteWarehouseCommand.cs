using Application.Features.DefinationManagementFeatures.Warehouses.Constants;
using Application.Features.DefinationManagementFeatures.Warehouses.Rules;
using Application.Repositories.DefinitonManagementRepos.WarehouseRepo;
using AutoMapper;
using Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Commands.Delete;

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
            Warehouse? warehouse = await _warehouseReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
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