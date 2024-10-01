using Application.Features.FinanceManagementFeatures.FinanceExpenses.Constants;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using Domain.Entities.OrganizationManagements;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Rules;

public class FinanceExpenseBusinessRules : BaseBusinessRules
{


    private readonly IFinanceExpenseGroupReadRepository _financeExpenseGroupReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly ICurrencyReadRepository _currencyReadRepository;
    private readonly IOrganizationReadRepository _organizationReadRepository;

    public FinanceExpenseBusinessRules(IFinanceExpenseGroupReadRepository financeExpenseGroupReadRepository, IUserReadRepository userReadRepository, ICurrencyReadRepository currencyReadRepository, IOrganizationReadRepository organizationReadRepository)
    {
        _financeExpenseGroupReadRepository = financeExpenseGroupReadRepository;
        _userReadRepository = userReadRepository;
        _currencyReadRepository = currencyReadRepository;
        _organizationReadRepository = organizationReadRepository;
    }

    public async Task FinanceExpenseShouldExistWhenSelected(X.FinanceExpense? item)
    {
        if (item == null)
            throw new BusinessException(FinanceExpensesBusinessMessages.FinanceExpenseNotExists);
    }

    public async Task FinanceExpenseGroupShouldExistWhenSelected(Guid gidExpenseGroupFK)
    {
        X.FinanceExpenseGroup financeExpenseGroup = await _financeExpenseGroupReadRepository.GetAsync(predicate: x => x.Gid == gidExpenseGroupFK);
        if (financeExpenseGroup == null)
            throw new BusinessException(FinanceExpensesBusinessMessages.FinanceExpenseGroupNotExists);
    }

    public async Task MoneySenderPersonnelShouldExistWhenSelected(Guid gidMoneySenderPersonnelFK)
    {
        User user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gidMoneySenderPersonnelFK);
        if (user == null)
            throw new BusinessException(FinanceExpensesBusinessMessages.MoneySenderPersonnelNotExists);
    }

    public async Task MoneyReceivePersonnelShouldExistWhenSelected(Guid gidMoneyReceivePersonnelFK)
    {
        User user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gidMoneyReceivePersonnelFK);
        if (user == null)
            throw new BusinessException(FinanceExpensesBusinessMessages.MoneyReceivePersonnelNotExists);
    }

    public async Task CurrencyShouldExistWhenSelected(Guid gidCurrencyFK)
    {
        Currency currency = await _currencyReadRepository.GetAsync(predicate: x => x.Gid == gidCurrencyFK);
        if (currency == null)
            throw new BusinessException(FinanceExpensesBusinessMessages.CurrencyNotExists);
    }

    public async Task ApprovalReceiverShouldExistWhenSelected(string? gidApprovalReceiverFK)
    {
        User user = await _userReadRepository.GetAsync(predicate: x => x.Gid.ToString() == gidApprovalReceiverFK);
        if (gidApprovalReceiverFK != null && user == null)
            throw new BusinessException(FinanceExpensesBusinessMessages.ApprovalReceiverNotExists);
    }

    public async Task OrganizationShouldExistWhenSelected(string? organizationGid)
    {
        Organization organization = await _organizationReadRepository.GetAsync(predicate: x => x.Gid.ToString() == organizationGid);

        if (organizationGid != null && organization == null)
            throw new BusinessException(FinanceExpensesBusinessMessages.OrganizationNotExists);
    }

}