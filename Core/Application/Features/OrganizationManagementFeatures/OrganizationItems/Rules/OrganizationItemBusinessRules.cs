using Application.Features.OrganizationManagementFeatures.OrganizationItems.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Rules;

public class OrganizationItemBusinessRules : BaseBusinessRules
{
    //public Guid GidOrganizationGroupFK { get; set; }
    // public Guid GidMainResponsibleUserFK { get; set; }

    private readonly IOrganizationGroupReadRepository _organizationGroupReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IOrganizationItemReadRepository _organizationItemReadRepository;

    public OrganizationItemBusinessRules(IOrganizationGroupReadRepository organizationGroupReadRepository, IUserReadRepository userReadRepository, IOrganizationItemReadRepository organizationItemReadRepository)
    {
        _organizationGroupReadRepository = organizationGroupReadRepository;
        _userReadRepository = userReadRepository;
        _organizationItemReadRepository = organizationItemReadRepository;
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

    public async Task DateRangeCheck(Guid gidOrganizationGroupFK, DateTime startDate, DateTime endDate)
    {
        X.OrganizationGroup? organizationGroup = await _organizationGroupReadRepository.GetAsync(predicate: x => x.Gid == gidOrganizationGroupFK, include: x => x.Include(x => x.OrganizationFK));
        if (organizationGroup == null)
            throw new BusinessException(OrganizationItemsBusinessMessages.OrganizationGroupNotExists);

        if (startDate < organizationGroup.OrganizationFK.StartDate || endDate > organizationGroup.OrganizationFK.EndDate || startDate > endDate)
            throw new BusinessException(OrganizationItemsBusinessMessages.DateRangeCheck);
    }

}