using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Rules;

public class TransportationPersonnelBusinessRules : BaseBusinessRules
{
    public async Task TransportationPersonnelShouldExistWhenSelected(X.TransportationPersonnel? item)
    {
        if (item == null)
            throw new BusinessException(TransportationPersonnelsBusinessMessages.TransportationPersonnelNotExists);
    }
}