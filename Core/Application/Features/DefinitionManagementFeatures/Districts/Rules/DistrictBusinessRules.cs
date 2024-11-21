using Application.Features.DefinitionManagementFeatures.Districts.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Districts.Rules;

public class DistrictBusinessRules : BaseBusinessRules
{
    public async Task DistrictShouldExistWhenSelected(X.District? item)
    {
        if (item == null)
            throw new BusinessException(DistrictsBusinessMessages.DistrictNotExists);
    }
}