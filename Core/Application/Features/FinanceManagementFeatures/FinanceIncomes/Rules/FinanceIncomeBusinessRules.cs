using Application.Features.FinanceManagementFeatures.FinanceIncomes.Constants;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.DefinitionManagements;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Rules;

public class FinanceIncomeBusinessRules : BaseBusinessRules
{
    //public Guid GidIncomeGroupFK { get; set; }
    //public Guid GidCurrencyFK { get; set; }

    private readonly IFinanceIncomeGroupReadRepository _financeIncomeGroupReadRepository;
    private readonly ICurrencyReadRepository _currencyReadRepository;

    public FinanceIncomeBusinessRules(IFinanceIncomeGroupReadRepository financeIncomeGroupReadRepository, ICurrencyReadRepository currencyReadRepository)
    {
        _financeIncomeGroupReadRepository = financeIncomeGroupReadRepository;
        _currencyReadRepository = currencyReadRepository;
    }

    public async Task FinanceIncomeShouldExistWhenSelected(X.FinanceIncome? item)
    {
        if (item == null)
            throw new BusinessException(FinanceIncomesBusinessMessages.FinanceIncomeNotExists);
    }

    public async Task FinanceIncomeGroupShouldExistWhenSelected(Guid gidIncomeGroupFK)
    {
        X.FinanceIncomeGroup? financeIncomeGroup = await _financeIncomeGroupReadRepository.GetAsync(predicate: x => x.Gid == gidIncomeGroupFK);
        if (financeIncomeGroup == null)
            throw new BusinessException(FinanceIncomesBusinessMessages.FinanceIncomeGroupNotExists);
    }

    public async Task CurrencyShouldExistWhenSelected(Guid gidCurrencyFK)
    {
        Currency? currency = await _currencyReadRepository.GetAsync(predicate: x => x.Gid == gidCurrencyFK);
        if (currency == null)
            throw new BusinessException(FinanceIncomesBusinessMessages.CurrencyNotExists);
    }

}