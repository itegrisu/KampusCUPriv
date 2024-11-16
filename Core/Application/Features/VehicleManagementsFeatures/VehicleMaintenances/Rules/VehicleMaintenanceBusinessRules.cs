using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Rules;

public class VehicleMaintenanceBusinessRules : BaseBusinessRules
{
    public async Task VehicleMaintenanceShouldExistWhenSelected(X.VehicleMaintenance? item)
    {
        if (item == null)
            throw new BusinessException(VehicleMaintenancesBusinessMessages.VehicleMaintenanceNotExists);
    }
}