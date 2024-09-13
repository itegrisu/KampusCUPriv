using Application.Features.LogManagementFeatures.LogEmailSends.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Rules;

public class LogEmailSendBusinessRules : BaseBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;

    public LogEmailSendBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task LogEmailSendShouldExistWhenSelected(X.LogEmailSend? item)
    {
        if (item == null)
            throw new BusinessException(LogEmailSendsBusinessMessages.LogEmailSendNotExists);
    }

    public async Task UserShouldExistWhenSelected(Guid gidUserFK)
    {
        var user = await _userReadRepository.GetAsync(predicate: x => x.Gid == gidUserFK);
        if (user == null)
            throw new BusinessException(LogEmailSendsBusinessMessages.UserNotExists);
    }

}