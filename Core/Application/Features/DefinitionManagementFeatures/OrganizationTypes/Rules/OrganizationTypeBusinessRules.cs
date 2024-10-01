using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Constants;
using Application.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Rules;

public class OrganizationTypeBusinessRules : BaseBusinessRules
{
    private readonly IOrganizationTypeReadRepository _organizationTypeReadRepository;

    public OrganizationTypeBusinessRules(IOrganizationTypeReadRepository organizationTypeReadRepository)
    {
        _organizationTypeReadRepository = organizationTypeReadRepository;
    }

    public async Task OrganizationTypeShouldExistWhenSelected(X.OrganizationType? item)
    {
        if (item == null)
            throw new BusinessException(OrganizationTypesBusinessMessages.OrganizationTypeNotExists);
    }

    public async Task OrganizationNameShouldBeUnique(string name, Guid? gid = null)
    {
        if (await _organizationTypeReadRepository.GetAsync(x => x.Name == name && x.Gid != gid) == null)
            throw new BusinessException(OrganizationTypesBusinessMessages.OrganizationTypeNameShouldBeUnique);
    }
}