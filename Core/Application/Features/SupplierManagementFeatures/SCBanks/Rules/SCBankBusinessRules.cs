using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Constants;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.DefinitionManagements;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Rules;

public class SCBankBusinessRules : BaseBusinessRules
{
    //public Guid GidSCCompanyFK { get; set; }
    //public Guid GidCurrencyFK { get; set; }

    private readonly ISCCompanyReadRepository _scCompanyReadRepository;
    private readonly ICurrencyReadRepository _currencyReadRepository;

    public SCBankBusinessRules(ISCCompanyReadRepository scCompanyReadRepository, ICurrencyReadRepository currencyReadRepository)
    {
        _scCompanyReadRepository = scCompanyReadRepository;
        _currencyReadRepository = currencyReadRepository;
    }

    public async Task SCBankShouldExistWhenSelected(X.SCBank? item)
    {
        if (item == null)
            throw new BusinessException(SCBanksBusinessMessages.SCBankNotExists);
    }

    public async Task SCCompanyShouldExistWhenSelected(Guid gid)
    {
        X.SCCompany? scCompany = await _scCompanyReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (scCompany == null)
            throw new BusinessException(SCBanksBusinessMessages.SCCompanyNotExists);
    }

    public async Task CurrencyShouldExistWhenSelected(Guid gid)
    {
        Currency? currency = await _currencyReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (currency == null)
            throw new BusinessException(SCBanksBusinessMessages.CurrencyNotExists);
    }
}