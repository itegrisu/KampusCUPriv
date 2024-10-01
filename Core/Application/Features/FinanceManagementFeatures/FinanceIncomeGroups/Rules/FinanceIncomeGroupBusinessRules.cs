using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Rules;

public class FinanceIncomeGroupBusinessRules : BaseBusinessRules
{
    public async Task FinanceIncomeGroupShouldExistWhenSelected(X.FinanceIncomeGroup? item)
    {
        if (item == null)
            throw new BusinessException(FinanceIncomeGroupsBusinessMessages.FinanceIncomeGroupNotExists);
    }
}