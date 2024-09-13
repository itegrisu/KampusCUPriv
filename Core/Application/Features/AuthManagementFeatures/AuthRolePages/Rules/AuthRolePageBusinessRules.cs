using Application.Features.AuthManagementFeatures.AuthRolePages.Constants;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.AuthManagements;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Rules;

public class AuthRolePageBusinessRules : BaseBusinessRules
{
    public Guid GidRoleFK { get; set; }
    public Guid GidPageFK { get; set; }

    private readonly IAuthRolePageReadRepository _authRolePageReadRepository;
    private readonly IAuthRoleReadRepository _authRoleReadRepository;
    private readonly IAuthPageReadRepository _authPageReadRepository;


    public AuthRolePageBusinessRules(IAuthRoleReadRepository authRoleReadRepository, IAuthPageReadRepository authPageReadRepository, IAuthRolePageReadRepository authRolePageReadRepository)
    {
        _authRoleReadRepository = authRoleReadRepository;
        _authPageReadRepository = authPageReadRepository;
        _authRolePageReadRepository = authRolePageReadRepository;
    }

    public async Task AuthRolePageShouldExistWhenSelected(Guid gid) // Role Page'in olup olmadýðýný kontrol eder.
    {
        if (await _authRolePageReadRepository.GetByGidAsync(gid) == null)
            throw new BusinessException(AuthRolePagesBusinessMessages.ErrorAuthRolePageShouldExist);
    }

    public async Task AuthRoleShouldExistWhenSelected(Guid gidRole) // Role'nin olup olmadýðýný kontrol eder.
    {
        if (await _authRoleReadRepository.GetByGidAsync(gidRole) == null)
            throw new BusinessException(AuthRolePagesBusinessMessages.ErrorAuthRoleShouldExist);
    }

    public async Task AuthPageShouldExistWhenSelected(Guid gidPage) // Page'in olup olmadýðýný kontrol eder.
    {
        if (await _authPageReadRepository.GetByGidAsync(gidPage) == null)
            throw new BusinessException(AuthRolePagesBusinessMessages.ErrorAuthPageShouldExist);
    }

    public async Task AuthPageHasBeenAddedBefore(Guid gidRole, Guid gidPage) // Page'in daha önce eklenip eklenmediðini kontrol eder.
    {
        if (await _authRolePageReadRepository.GetAsync(predicate: x => x.GidRoleFK == gidRole && x.GidPageFK == gidPage) != null)
            throw new BusinessException(AuthRolePagesBusinessMessages.AuthPageHasBeenAddedBefore);
    }
}