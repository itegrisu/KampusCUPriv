using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Rules;

public class PersonnelResidenceInfoBusinessRules : BaseBusinessRules
{
    public async Task PersonnelResidenceInfoShouldExistWhenSelected(X.PersonnelResidenceInfo? item)
    {
        if (item == null)
            throw new BusinessException(PersonnelResidenceInfosBusinessMessages.PersonnelResidenceInfoNotExists);
    }
}