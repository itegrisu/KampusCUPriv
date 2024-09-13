using Application.Features.PortalManagementFeatures.PortalParameters.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PortalManagements;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Rules;

public class PortalParameterBusinessRules : BaseBusinessRules
{
    public async Task PortalParameterShouldExistWhenSelected(X.PortalParameter? item)
    {
        if (item == null)
            throw new BusinessException(PortalParametersBusinessMessages.PortalParameterNotExists);
    }
}