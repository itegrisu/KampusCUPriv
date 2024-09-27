using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Constants;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Rules;

public class SCCompanyBusinessRules : BaseBusinessRules
{


    private readonly ISCCompanyReadRepository _scCompanyReadRepository;

    public SCCompanyBusinessRules(ISCCompanyReadRepository scCompanyReadRepository)
    {
        _scCompanyReadRepository = scCompanyReadRepository;
    }

    public async Task SCCompanyShouldExistWhenSelected(X.SCCompany? item)
    {
        if (item == null)
            throw new BusinessException(SCCompaniesBusinessMessages.SCCompanyNotExists);
    }


    public async Task SCCompanyShouldUnique(string companyName, Guid? companyGid = null)
    {
        X.SCCompany? scCompany = await _scCompanyReadRepository.GetAsync(predicate: x => x.CompanyName == companyName && x.Gid != companyGid);
        if (scCompany != null)
            throw new BusinessException(SCCompaniesBusinessMessages.SCCompanyAlreadyExists);
    }

}