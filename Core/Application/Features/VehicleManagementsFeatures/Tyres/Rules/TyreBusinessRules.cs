using Application.Features.VehicleManagementFeatures.Tyres.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.Tyres.Rules;

public class TyreBusinessRules : BaseBusinessRules
{
    public async Task TyreShouldExistWhenSelected(X.Tyre? item)
    {
        if (item == null)
            throw new BusinessException(TyresBusinessMessages.TyreNotExists);
    }
}