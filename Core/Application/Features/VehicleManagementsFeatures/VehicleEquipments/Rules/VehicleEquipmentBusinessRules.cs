using Application.Features.VehicleManagementFeatures.VehicleEquipments.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Rules;

public class VehicleEquipmentBusinessRules : BaseBusinessRules
{
    public async Task VehicleEquipmentShouldExistWhenSelected(X.VehicleEquipment? item)
    {
        if (item == null)
            throw new BusinessException(VehicleEquipmentsBusinessMessages.VehicleEquipmentNotExists);
    }
}