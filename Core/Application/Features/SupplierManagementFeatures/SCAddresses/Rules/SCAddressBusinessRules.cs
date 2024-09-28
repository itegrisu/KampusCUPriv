using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Constants;
using Application.Repositories.DefinitionManagementRepos.CityRepo;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.DefinitionManagements;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Rules;

public class SCAddressBusinessRules : BaseBusinessRules
{
    //public Guid GidSCCompanyFK { get; set; }
    //public Guid GidCityFK { get; set; }

    private readonly ISCCompanyReadRepository _scCompanyReadRepository;
    private readonly ICityReadRepository _cityReadRepository;

    public async Task SCAddressShouldExistWhenSelected(X.SCAddress? item)
    {
        if (item == null)
            throw new BusinessException(SCAddressesBusinessMessages.SCAddressNotExists);
    }


    public async Task SCCompanyShouldExistWhenSelected(Guid gidSCCompanyFK)
    {
        X.SCCompany? scCompany = await _scCompanyReadRepository.GetAsync(predicate: x => x.Gid == gidSCCompanyFK);
        if (scCompany == null)
            throw new BusinessException(SCAddressesBusinessMessages.SCCompanyNotExists);
    }

    public async Task CityShouldExistWhenSelected(Guid gidCityFK)
    {
        City? city = await _cityReadRepository.GetAsync(predicate: x => x.Gid == gidCityFK);
        if (city == null)
            throw new BusinessException(SCAddressesBusinessMessages.CityNotExists);
    }
}