using Application.Features.GeneralManagementFeatures.UserShortCuts.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Rules;

public class UserShortCutBusinessRules : BaseBusinessRules
{
    public Guid GidUserFK { get; set; }

    public readonly IUserReadRepository _userReadRepository;

    public UserShortCutBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task UserShortCutShouldExistWhenSelected(UserShortCut? item)
    {
        if (item == null)
            throw new BusinessException(UserShortCutsBusinessMessages.UserShortCutNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid UserGid)
    {
        if (await _userReadRepository.GetAsync(x => x.Gid == UserGid) == null)
        {
            throw new BusinessException(UserShortCutsBusinessMessages.UserNotExist);
        }
    }
}