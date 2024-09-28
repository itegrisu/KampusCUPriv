using Application.Features.GeneralManagementFeatures.UserModuleAuths.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Rules;

public class UserModuleAuthBusinessRules : BaseBusinessRules
{
    //GidUserFK should exist
    private readonly IUserReadRepository _userReadRepository;

    public UserModuleAuthBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
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


}