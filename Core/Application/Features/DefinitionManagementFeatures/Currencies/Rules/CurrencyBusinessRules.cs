using Application.Features.DefinitionManagementFeatures.Currencies.Constants;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Rules;

public class CurrencyBusinessRules : BaseBusinessRules
{

    private readonly ICurrencyReadRepository _currencyReadRepository;

    public CurrencyBusinessRules(ICurrencyReadRepository currencyReadRepository)
    {
        _currencyReadRepository = currencyReadRepository;
    }
    public async Task CurrencyShouldExistWhenSelected(X.Currency? item)
    {
        if (item == null)
            throw new BusinessException(CurrenciesBusinessMessages.CurrencyNotExists);
    }

    public async Task CheckCurrencyNameIsUnique(string currencyName, Guid? currencyGuid = null)
    {
        var currency = await _currencyReadRepository.GetAsync(predicate: x => x.Name.ToLower() == currencyName.ToLower() && (currencyGuid == null || x.Gid != currencyGuid));
        if (currency != null)
            throw new BusinessException(CurrenciesBusinessMessages.CurrencyIsAlreadyExists);
    }

    public async Task CheckCurrencyCodeIsUnique(string currencyCode, Guid? currencyGuid = null)
    {
        if (string.IsNullOrEmpty(currencyCode))
            return;
        var currency = await _currencyReadRepository.GetAsync(predicate: x => x.Code.ToLower() == currencyCode.ToLower() && (currencyGuid == null || x.Gid != currencyGuid));
        if (currency != null)
            throw new BusinessException(CurrenciesBusinessMessages.CurrencyCodeIsAlreadyExists);
    }

    public async Task CheckCurrencySymbolIsUnique(string currencySymbol, Guid? currencyGuid = null)
    {
        if (string.IsNullOrEmpty(currencySymbol))
            return;
        var currency = await _currencyReadRepository.GetAsync(predicate: x => x.Symbol.ToLower() == currencySymbol.ToLower() && (currencyGuid == null || x.Gid != currencyGuid));
        if (currency != null)
            throw new BusinessException(CurrenciesBusinessMessages.CurrencySymbolIsAlreadyExists);
    }

}