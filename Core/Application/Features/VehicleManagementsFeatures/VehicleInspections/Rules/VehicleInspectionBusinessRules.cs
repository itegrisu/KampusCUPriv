using Application.Features.VehicleManagementFeatures.VehicleInspections.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Rules;

public class VehicleInspectionBusinessRules : BaseBusinessRules
{
    public async Task VehicleInspectionShouldExistWhenSelected(X.VehicleInspection? item)
    {
        if (item == null)
            throw new BusinessException(VehicleInspectionsBusinessMessages.VehicleInspectionNotExists);
    }
}