using Application.Features.TransportationManagementFeatures.TransportationPassengers.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Rules;

public class TransportationPassengerBusinessRules : BaseBusinessRules
{
    public async Task TransportationPassengerShouldExistWhenSelected(X.TransportationPassenger? item)
    {
        if (item == null)
            throw new BusinessException(TransportationPassengersBusinessMessages.TransportationPassengerNotExists);
    }
}