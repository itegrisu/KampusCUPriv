using Application.Features.LogManagementFeatures.LogFailedLogins.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Rules;

public class LogFailedLoginBusinessRules : BaseBusinessRules
{
    public async Task LogFailedLoginShouldExistWhenSelected(X.LogFailedLogin? item)
    {
        if (item == null)
            throw new BusinessException(LogFailedLoginsBusinessMessages.LogFailedLoginNotExists);
    }
}