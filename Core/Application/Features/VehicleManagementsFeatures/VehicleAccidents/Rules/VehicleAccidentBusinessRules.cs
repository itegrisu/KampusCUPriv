using Application.Features.VehicleManagementFeatures.VehicleAccidents.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Rules;

public class VehicleAccidentBusinessRules : BaseBusinessRules
{
    public async Task VehicleAccidentShouldExistWhenSelected(X.VehicleAccident? item)
    {
        if (item == null)
            throw new BusinessException(VehicleAccidentsBusinessMessages.VehicleAccidentNotExists);
    }
}