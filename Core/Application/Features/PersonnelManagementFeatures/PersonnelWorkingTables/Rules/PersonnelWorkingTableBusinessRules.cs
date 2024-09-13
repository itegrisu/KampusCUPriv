using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Rules;

public class PersonnelWorkingTableBusinessRules : BaseBusinessRules
{
    public async Task PersonnelWorkingTableShouldExistWhenSelected(X.PersonnelWorkingTable? item)
    {
        if (item == null)
            throw new BusinessException(PersonnelWorkingTablesBusinessMessages.PersonnelWorkingTableNotExists);
    }
}