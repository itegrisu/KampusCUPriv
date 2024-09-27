using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Constants;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Rules;

public class SCEmployerBusinessRules : BaseBusinessRules
{
    //GidSCCompanyFK

    private readonly ISCCompanyReadRepository _scCompanyReadRepository;

    public SCEmployerBusinessRules(ISCCompanyReadRepository scCompanyReadRepository)
    {
        _scCompanyReadRepository = scCompanyReadRepository;
    }

    public async Task SCEmployerShouldExistWhenSelected(X.SCEmployer? item)
    {
        if (item == null)
            throw new BusinessException(SCEmployersBusinessMessages.SCEmployerNotExists);
    }

    public async Task SCCompanyShouldExistWhenSelected(Guid gid)
    {
        X.SCCompany? scCompany = await _scCompanyReadRepository.GetAsync(predicate: x => x.Gid == gid);
        if (scCompany == null)
            throw new BusinessException(SCEmployersBusinessMessages.SCCompanyNotExists);
    }

}