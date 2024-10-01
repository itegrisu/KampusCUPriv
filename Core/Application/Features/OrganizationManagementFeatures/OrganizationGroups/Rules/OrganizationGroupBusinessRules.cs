using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Constants;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Rules;

public class OrganizationGroupBusinessRules : BaseBusinessRules
{
    //public Guid GidOrganizationFK { get; set; }
    //public string GroupName { get; set; }

    private readonly IOrganizationGroupReadRepository _organizationGroupReadRepository;
    private readonly IOrganizationReadRepository organizationReadRepository;

    public OrganizationGroupBusinessRules(IOrganizationGroupReadRepository organizationGroupReadRepository, IOrganizationReadRepository organizationReadRepository)
    {
        _organizationGroupReadRepository = organizationGroupReadRepository;
        this.organizationReadRepository = organizationReadRepository;
    }

    public async Task OrganizationGroupShouldExistWhenSelected(X.OrganizationGroup? item)
    {
        if (item == null)
            throw new BusinessException(OrganizationGroupsBusinessMessages.OrganizationGroupNotExists);
    }

    public async Task OrganizationShouldExistWhenSelected(Guid gidOrganizationFK)
    {
        var organization = await organizationReadRepository.GetAsync(predicate: x => x.Gid == gidOrganizationFK);
        if (organization == null)
            throw new BusinessException(OrganizationGroupsBusinessMessages.OrganizationNotExists);
    }

    public async Task OrganizationGroupNameShouldUniqe(string groupName, Guid? gidOrganizationFK = null)
    {
        var organizationGroup = await _organizationGroupReadRepository.GetAsync(predicate: x => x.GroupName == groupName && x.GidOrganizationFK == gidOrganizationFK);
        if (organizationGroup != null)
            throw new BusinessException(OrganizationGroupsBusinessMessages.OrganizationGroupNameShouldUniqe);
    }

}