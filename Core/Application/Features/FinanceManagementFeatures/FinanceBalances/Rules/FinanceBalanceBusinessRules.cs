using Application.Features.FinanceManagementFeatures.FinanceBalances.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Rules;

public class FinanceBalanceBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;

    public FinanceBalanceBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task FinanceBalanceShouldExistWhenSelected(X.FinanceBalance? item)
    {
        if (item == null)
            throw new BusinessException(FinanceBalancesBusinessMessages.FinanceBalanceNotExists);
    }

    public async Task isSystemAdmin(Guid userGid)
    {
        var user = await _userReadRepository.GetByGidAsync(userGid);
        if(user.IsSystemAdmin == false)
        {
            throw new BusinessException(FinanceBalancesBusinessMessages.UserNotSystemAdmin);
        }
    }
}