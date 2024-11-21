using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Rules;

public class TransportationExternalServiceBusinessRules : BaseBusinessRules
{
    public async Task TransportationExternalServiceShouldExistWhenSelected(X.TransportationExternalService? item)
    {
        if (item == null)
            throw new BusinessException(TransportationExternalServicesBusinessMessages.TransportationExternalServiceNotExists);
    }
}