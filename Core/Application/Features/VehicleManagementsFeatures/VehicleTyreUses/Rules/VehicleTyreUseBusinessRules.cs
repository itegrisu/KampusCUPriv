using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Rules;

public class VehicleTyreUseBusinessRules : BaseBusinessRules
{
    public async Task VehicleTyreUseShouldExistWhenSelected(X.VehicleTyreUse? item)
    {
        if (item == null)
            throw new BusinessException(VehicleTyreUsesBusinessMessages.VehicleTyreUseNotExists);
    }
}