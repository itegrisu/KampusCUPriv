using Application.Features.DefinitionManagementFeatures.PermitTypes.Constants;
using Application.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Rules;

public class PermitTypeBusinessRules : BaseBusinessRules
{

    private readonly IPermitTypeReadRepository _permitTypeReadRepository;

    public PermitTypeBusinessRules(IPermitTypeReadRepository permitTypeReadRepository)
    {
        _permitTypeReadRepository = permitTypeReadRepository;
    }

    public async Task PermitTypeShouldExistWhenSelected(X.PermitType? item)
    {
        if (item == null)
            throw new BusinessException(PermitTypesBusinessMessages.PermitTypeNotExists);
    }

    public async Task CheckPermitTypeNameIsUnique(string permitTypeName, Guid? permitTypeGuid = null)
    {
        var permitType = await _permitTypeReadRepository.GetAsync(predicate: x => x.IzinAdi.ToLower() == permitTypeName.ToLower() && (permitTypeGuid == null || x.Gid != permitTypeGuid));
        if (permitType != null)
            throw new BusinessException(PermitTypesBusinessMessages.PermitTypeIsAlreadyExists);
    }

}