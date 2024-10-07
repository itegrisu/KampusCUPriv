using Application.Features.GeneralManagementFeatures.UserModuleAuths.Constants;
using Application.Repositories.GeneralManagementRepos.UserModuleAuthRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Enums;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Rules;

public class UserModuleAuthBusinessRules : BaseBusinessRules
{
    //GidUserFK should exist
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserModuleAuthReadRepository _userModuleAuthReadRepository;
    public UserModuleAuthBusinessRules(IUserReadRepository userReadRepository, IUserModuleAuthReadRepository userModuleAuthReadRepository)
    {
        _userReadRepository = userReadRepository;
        _userModuleAuthReadRepository = userModuleAuthReadRepository;
    }

    public async Task UserModuleAuthShouldExistWhenSelected(X.UserModuleAuth? item)
    {
        if (item == null)
            throw new BusinessException(UserModuleAuthsBusinessMessages.UserModuleAuthNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid gidUserFK)
    {
        X.User? user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gidUserFK);
        if (user == null)
            throw new BusinessException(UserModuleAuthsBusinessMessages.UserNotExists);
    }

    public async Task UserAllreadyAssignedAuthorities(Guid gidUserFK, EnumModuleType moduleType)
    {
        X.UserModuleAuth? userModuleAuth = await _userModuleAuthReadRepository.GetAsync(predicate: x => x.UserFK.Gid == gidUserFK && x.ModuleType == moduleType);
        if (userModuleAuth != null)
            throw new BusinessException(UserModuleAuthsBusinessMessages.UserAllreadyAssignedAuthorities);
    }


}