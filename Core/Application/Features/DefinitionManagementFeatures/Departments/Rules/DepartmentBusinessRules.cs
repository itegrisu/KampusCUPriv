using Application.Features.DefinitionFeatures.Departments.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionFeatures.Departments.Rules;

public class DepartmentBusinessRules : BaseBusinessRules
{
    public async Task DepartmentShouldExistWhenSelected(X.Department? item)
    {
        if (item == null)
            throw new BusinessException(DepartmentsBusinessMessages.DepartmentNotExists);
    }
}