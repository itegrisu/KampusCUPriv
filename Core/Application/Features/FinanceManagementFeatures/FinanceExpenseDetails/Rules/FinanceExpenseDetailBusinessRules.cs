using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Constants;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Rules;

public class FinanceExpenseDetailBusinessRules : BaseBusinessRules
{
    //public Guid GidExpenseFK { get; set; }
    //public Guid GidSpendPersonnelFK { get; set; }
    //public Guid GidCurrencyFK { get; set; }
    //public string? GidControlPersonnelFK { get; set; }

    private readonly IFinanceExpenseReadRepository _financeExpenceReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly ICurrencyReadRepository _currencyReadRepository;

    public FinanceExpenseDetailBusinessRules(IFinanceExpenseReadRepository financeExpenceReadRepository, IUserReadRepository userReadRepository, ICurrencyReadRepository currencyReadRepository)
    {
        _financeExpenceReadRepository = financeExpenceReadRepository;
        _userReadRepository = userReadRepository;
        _currencyReadRepository = currencyReadRepository;
    }

    public async Task FinanceExpenseDetailShouldExistWhenSelected(X.FinanceExpenseDetail? item)
    {
        if (item == null)
            throw new BusinessException(FinanceExpenseDetailsBusinessMessages.FinanceExpenseDetailNotExists);
    }

    public async Task FinanceExpenseShouldExistWhenSelected(Guid gidExpenseFK)
    {
        X.FinanceExpense financeExpense = await _financeExpenceReadRepository.GetAsync(predicate: x => x.Gid == gidExpenseFK);
        if (financeExpense == null)
            throw new BusinessException(FinanceExpenseDetailsBusinessMessages.FinanceExpenseNotExists);
    }

    public async Task SpendPersonnelShouldExistWhenSelected(Guid gidSpendPersonnelFK)
    {
        User user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gidSpendPersonnelFK);
        if (user == null)
            throw new BusinessException(FinanceExpenseDetailsBusinessMessages.SpendPersonnelNotExists);
    }

    public async Task CurrencyShouldExistWhenSelected(Guid gidCurrencyFK)
    {
        Currency currency = await _currencyReadRepository.GetAsync(predicate: x => x.Gid == gidCurrencyFK);
        if (currency == null)
            throw new BusinessException(FinanceExpenseDetailsBusinessMessages.CurrencyNotExists);
    }

    public async Task ControlPersonnelShouldExistWhenSelected(string? gidControlPersonnelFK)
    {
        User user = await _userReadRepository.GetAsync(predicate: x => x.Gid.ToString() == gidControlPersonnelFK);
        if (gidControlPersonnelFK != null && user == null)
            throw new BusinessException(FinanceExpenseDetailsBusinessMessages.ControlPersonnelNotExists);
    }
}