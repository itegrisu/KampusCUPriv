using Application.Features.TransportationManagementFeatures.TransportationServices.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Rules;

public class TransportationServiceBusinessRules : BaseBusinessRules
{
    public async Task TransportationServiceShouldExistWhenSelected(X.TransportationService? item)
    {
        if (item == null)
            throw new BusinessException(TransportationServicesBusinessMessages.TransportationServiceNotExists);
    }
}