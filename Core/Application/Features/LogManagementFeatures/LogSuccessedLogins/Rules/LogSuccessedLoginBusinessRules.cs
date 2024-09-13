using Application.Features.LogManagementFeatures.LogSuccessedLogins.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Rules;

public class LogSuccessedLoginBusinessRules : BaseBusinessRules
{
    public Guid UserGid { get; set; }
    private readonly IUserReadRepository _userReadRepository;

    public LogSuccessedLoginBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task LogSuccessedLoginShouldExistWhenSelected(X.LogSuccessedLogin? item)
    {
        if (item == null)
            throw new BusinessException(LogSuccessedLoginsBusinessMessages.LogSuccessedLoginNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid userGid)
    {
        var user = await _userReadRepository.GetAsync(predicate: u => u.Gid == userGid);
        if (user == null)
            throw new BusinessException(LogSuccessedLoginsBusinessMessages.UserNotExists);
    }
}