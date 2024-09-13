using Application.Features.PortalManagementFeatures.PortalTexts.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PortalManagements;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Rules;

public class PortalTextBusinessRules : BaseBusinessRules
{
    public async Task PortalTextShouldExistWhenSelected(X.PortalText? item)
    {
        if (item == null)
            throw new BusinessException(PortalTextsBusinessMessages.PortalTextNotExists);
    }
}