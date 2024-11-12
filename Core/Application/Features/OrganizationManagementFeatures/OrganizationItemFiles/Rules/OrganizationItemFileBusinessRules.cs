using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Rules;

public class OrganizationItemFileBusinessRules : BaseBusinessRules
{
    public async Task OrganizationItemFileShouldExistWhenSelected(X.OrganizationItemFile? item)
    {
        if (item == null)
            throw new BusinessException(OrganizationItemFilesBusinessMessages.OrganizationItemFileNotExists);
    }
}