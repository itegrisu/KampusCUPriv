using Application.Features.OrganizationManagementFeatures.OrganizationItems.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.GeneralManagements;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Rules;

public class OrganizationItemBusinessRules : BaseBusinessRules
{
    //public Guid GidOrganizationGroupFK { get; set; }
    // public Guid GidMainResponsibleUserFK { get; set; }

    private readonly IOrganizationGroupReadRepository _organizationGroupReadRepository;
    private readonly IUserReadRepository _userReadRepository;

    public OrganizationItemBusinessRules(IOrganizationGroupReadRepository organizationGroupReadRepository, IUserReadRepository userReadRepository)
    {
        _organizationGroupReadRepository = organizationGroupReadRepository;
        _userReadRepository = userReadRepository;
    }

    public async Task OrganizationItemShouldExistWhenSelected(X.OrganizationItem? item)
    {
        if (item == null)
            throw new BusinessException(OrganizationItemsBusinessMessages.OrganizationItemNotExists);
    }

    public async Task OrganizationGroupShouldExistWhenSelected(Guid gidOrganizationGroupFK)
    {
        X.OrganizationGroup organizationGroup = await _organizationGroupReadRepository.GetAsync(predicate: x => x.Gid == gidOrganizationGroupFK);
        if (organizationGroup == null)
            throw new BusinessException(OrganizationItemsBusinessMessages.OrganizationGroupNotExists);
    }

    public async Task MainResponsibleUserShouldExistWhenSelected(string? gidMainResponsibleUserFK)
    {
        if (gidMainResponsibleUserFK != null)
        {
            User user = await _userReadRepository.GetAsync(predicate: x => x.Gid.ToString() == gidMainResponsibleUserFK);
            if (user == null)
                throw new BusinessException(OrganizationItemsBusinessMessages.MainResponsibleUserNotExists);
        }
    }

}