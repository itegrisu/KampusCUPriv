using Application.Features.FinanceManagementFeatures.FinanceBalances.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Rules;

public class FinanceBalanceBusinessRules : BaseBusinessRules
{
    public async Task FinanceBalanceShouldExistWhenSelected(X.FinanceBalance? item)
    {
        if (item == null)
            throw new BusinessException(FinanceBalancesBusinessMessages.FinanceBalanceNotExists);
    }
}