using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Constants;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Rules;

public class FinanceExpenseGroupBusinessRules : BaseBusinessRules
{
    //Name should be unique
    private readonly IFinanceExpenseGroupReadRepository _financeExpenceGroupReadRepository;

    public FinanceExpenseGroupBusinessRules(IFinanceExpenseGroupReadRepository financeExpenceGroupReadRepository)
    {
        _financeExpenceGroupReadRepository = financeExpenceGroupReadRepository;
    }

    public async Task FinanceExpenseGroupShouldExistWhenSelected(X.FinanceExpenseGroup? item)
    {
        if (item == null)
            throw new BusinessException(FinanceExpenseGroupsBusinessMessages.FinanceExpenseGroupNotExists);
    }

    public async Task FinanceExpenseGroupShouldUnique(string name, Guid? gid = null)
    {
        X.FinanceExpenseGroup financeExpenseGroup = await _financeExpenceGroupReadRepository.GetAsync(predicate: x => x.Name == name && x.Gid != gid);
        if (financeExpenseGroup != null)
            throw new BusinessException(FinanceExpenseGroupsBusinessMessages.FinanceExpenseGroupAlreadyExists);
    }

}