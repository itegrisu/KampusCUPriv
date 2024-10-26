using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Rules;

public class OrganizationFileBusinessRules : BaseBusinessRules
{
    public async Task OrganizationFileShouldExistWhenSelected(X.OrganizationFile? item)
    {
        if (item == null)
            throw new BusinessException(OrganizationFilesBusinessMessages.OrganizationFileNotExists);
    }
}