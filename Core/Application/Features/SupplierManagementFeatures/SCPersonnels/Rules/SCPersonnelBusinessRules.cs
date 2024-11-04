using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Constants;
using Application.Repositories.SupplierManagementRepos.SCPersonnelRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Rules;

public class SCPersonnelBusinessRules : BaseBusinessRules
{
    private readonly ISCPersonnelReadRepository _sCPersonnelReadRepository;

    public SCPersonnelBusinessRules(ISCPersonnelReadRepository sCPersonnelReadRepository)
    {
        _sCPersonnelReadRepository = sCPersonnelReadRepository;
    }

    public async Task SCPersonnelShouldExistWhenSelected(X.SCPersonnel? item)
    {
        if (item == null)
            throw new BusinessException(SCPersonnelsBusinessMessages.SCPersonnelNotExists);
    }
    public async Task SCPersonnelShouldNotBeDuplicated(Guid personnelGid, Guid companyGid)
    {
        var existingPersonnel = await _sCPersonnelReadRepository.GetAsync(p => p.GidPersonnelFK == personnelGid && p.GidSCCompanyFK == companyGid);
        if (existingPersonnel != null)
            throw new BusinessException(SCPersonnelsBusinessMessages.SCPersonnelAlreadyExists);
    }


}