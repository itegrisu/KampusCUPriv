using Application.Features.TransportationManagementFeatures.TransportationGroups.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Rules;

public class TransportationGroupBusinessRules : BaseBusinessRules
{
    public async Task TransportationGroupShouldExistWhenSelected(X.TransportationGroup? item)
    {
        if (item == null)
            throw new BusinessException(TransportationGroupsBusinessMessages.TransportationGroupNotExists);
    }
}