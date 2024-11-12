using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Rules;

public class OrganizationItemMessageBusinessRules : BaseBusinessRules
{
    public async Task OrganizationItemMessageShouldExistWhenSelected(X.OrganizationItemMessage? item)
    {
        if (item == null)
            throw new BusinessException(OrganizationItemMessagesBusinessMessages.OrganizationItemMessageNotExists);
    }
}