using Application.Features.DefinitionManagementFeatures.TyreTypes.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Rules;

public class TyreTypeBusinessRules : BaseBusinessRules
{
    public async Task TyreTypeShouldExistWhenSelected(X.TyreType? item)
    {
        if (item == null)
            throw new BusinessException(TyreTypesBusinessMessages.TyreTypeNotExists);
    }
}