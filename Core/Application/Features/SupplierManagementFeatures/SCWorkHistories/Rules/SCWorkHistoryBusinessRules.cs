using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Constants;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Rules;

public class SCWorkHistoryBusinessRules : BaseBusinessRules
{
    //GidSCCompanyFK

    private readonly ISCCompanyReadRepository _sCCompanyreadRepository;

    public SCWorkHistoryBusinessRules(ISCCompanyReadRepository sCCompanyreadRepository)
    {
        _sCCompanyreadRepository = sCCompanyreadRepository;
    }

    public async Task SCWorkHistoryShouldExistWhenSelected(X.SCWorkHistory? item)
    {
        if (item == null)
            throw new BusinessException(SCWorkHistoriesBusinessMessages.SCWorkHistoryNotExists);
    }

    public async Task SCCompanyShouldExistWhenSelected(Guid gidSCCompanyFK)
    {
        var sCCompany = await _sCCompanyreadRepository.GetByGidAsync(gidSCCompanyFK);
        if (sCCompany == null)
            throw new BusinessException(SCWorkHistoriesBusinessMessages.SCCompanyNotExists);
    }


}