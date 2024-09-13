using Application.Features.AuthManagementFeatures.AuthRolePages.Constants;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Constants;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Azure.Core;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Rules;

public class AuthUserRoleBusinessRules : BaseBusinessRules
{
    public Guid GidUserFK { get; set; }
    public Guid GidRoleFK { get; set; }
    public Guid GidPageFK { get; set; }

    private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IAuthRoleReadRepository _authRoleReadRepository;
    private readonly IAuthPageReadRepository _authPageReadRepository;

    public AuthUserRoleBusinessRules(IAuthUserRoleReadRepository authUserRoleReadRepository, IUserReadRepository userReadRepository, IAuthRoleReadRepository authRoleReadRepository, IAuthPageReadRepository authPageReadRepository)
    {
        _authUserRoleReadRepository = authUserRoleReadRepository;
        _userReadRepository = userReadRepository;
        _authRoleReadRepository = authRoleReadRepository;
        _authPageReadRepository = authPageReadRepository;
    }

    public async Task AuthUserRoleShouldExistWhenSelected(Guid gid)
    {
        if (await _authUserRoleReadRepository.GetByGidAsync(gid) == null)
            throw new BusinessException(AuthUserRolesBusinessMessages.ErrorAuthUserRoleShouldExist);
    }

    public async Task UserShouldExistWhenSelected(Guid gidUser)
    {
        if (await _userReadRepository.GetByGidAsync(gidUser) == null)
            throw new BusinessException(AuthUserRolesBusinessMessages.ErrorUserShouldExist);
    }

    public async Task RoleShouldExistWhenSelected(Guid? gidRole)
    {
        if (gidRole != null && await _authRoleReadRepository.GetAsync(predicate: x => x.Gid == gidRole) == null)
            throw new BusinessException(AuthUserRolesBusinessMessages.ErrorRoleShouldExist);
    }

    public async Task PageShouldExistWhenSelected(Guid? gidPage)
    {
        if (gidPage != null && await _authPageReadRepository.GetAsync(predicate: x => x.Gid == gidPage) == null)
            throw new BusinessException(AuthUserRolesBusinessMessages.ErrorPageShouldExist);
    }

    public async Task AuthRoleHasBeenAddedBefore(Guid? gidRole, Guid gidUser) // Rol'ün daha önce eklenip eklenmediðini kontrol eder.
    {
        if (gidRole != null && await _authUserRoleReadRepository.GetAsync(predicate: x => x.GidRoleFK == gidRole && x.GidUserFK == gidUser) != null)
            throw new BusinessException(AuthUserRolesBusinessMessages.AuthRoleHasBeenAddedBefore);
    }

    public async Task<bool> AuthRoleHasBeenAddedBeforeByCheckBox(Guid gidRole, Guid gidUser) // Rol'ün daha önce eklenip eklenmediðini kontrol eder.
    {
        if (await _authUserRoleReadRepository.GetAsync(predicate: x => x.GidRoleFK == gidRole && x.GidUserFK == gidUser) != null)
            return true;
        return false;
    }

    public async Task AuthPageHasBeenAddedBefore(Guid? gidPage, Guid gidUser) // Sayfanýn daha önce eklenip eklenmediðini kontrol eder.
    {
        if (gidPage != null && await _authUserRoleReadRepository.GetAsync(predicate: x => x.GidPageFK == gidPage && x.GidUserFK == gidUser) != null)
            throw new BusinessException(AuthUserRolesBusinessMessages.AuthPageHasBeenAddedBefore);
    }

    public async Task DoesUserHasPage(Guid gidUser, Guid? gidPage)
    {
        if (gidPage == null) return;

        IPaginate<AuthUserRole> roles;
        int size = _authUserRoleReadRepository.CountFull();

        roles = await _authUserRoleReadRepository.GetListAsync(
                      index: 0,
                      size: size,
                      include: source => source.Include(x => x.AuthRoleFK).ThenInclude(x => x.AuthRolePages).ThenInclude(x => x.AuthPageFK).Include(x => x.UserFK).Include(x => x.AuthPageFK),
                      predicate: x => x.UserFK.Gid == gidUser);

        List<AuthPage> pages = roles.Items.
                                    Where(x => x.AuthRoleFK != null)
                                   .SelectMany(x => x.AuthRoleFK.AuthRolePages)
                                   .Select(x => x.AuthPageFK)
                                   .Distinct().ToList();

        if (pages.Any(p => p.Gid == gidPage))
            throw new BusinessException(AuthUserRolesBusinessMessages.DoesUserHasPage);


    }
}