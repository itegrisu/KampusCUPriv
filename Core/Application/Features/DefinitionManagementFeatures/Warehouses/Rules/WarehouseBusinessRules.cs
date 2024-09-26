using Application.Features.DefinationManagementFeatures.Warehouses.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinationManagementFeatures.Warehouses.Rules;

public class WarehouseBusinessRules : BaseBusinessRules
{
    public async Task WarehouseShouldExistWhenSelected(Warehouse? item)
    {
        if (item == null)
            throw new BusinessException(WarehousesBusinessMessages.WarehouseNotExists);
    }
}