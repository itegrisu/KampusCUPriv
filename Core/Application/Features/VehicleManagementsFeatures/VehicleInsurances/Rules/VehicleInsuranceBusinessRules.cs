using Application.Features.VehicleManagementFeatures.VehicleInsurances.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Rules;

public class VehicleInsuranceBusinessRules : BaseBusinessRules
{
    public async Task VehicleInsuranceShouldExistWhenSelected(X.VehicleInsurance? item)
    {
        if (item == null)
            throw new BusinessException(VehicleInsurancesBusinessMessages.VehicleInsuranceNotExists);
    }
}