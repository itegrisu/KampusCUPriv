using Domain.Entities.AuthManagements;
using Core.CrossCuttingConcern.Exceptions;
using Core.Application;
using Application.Features.AuthManagementFeatures.AuthRoles.Constants;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Rules;

public class AuthRoleBusinessRules : BaseBusinessRules
{
    private readonly IAuthRoleReadRepository _authRoleReadRepository;


    public AuthRoleBusinessRules(IAuthRoleReadRepository authRoleReadRepository)
    {
        _authRoleReadRepository = authRoleReadRepository;
    }

    public async Task AuthRoleShouldExistWhenSelected(Guid gid)
    {
        if (await _authRoleReadRepository.GetByGidAsync(gid) == null)
            throw new BusinessException(AuthRolesBusinessMessages.AuthRoleNotExists);
       
    }


}