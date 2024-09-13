using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Rules;

public class LogAuthorizationErrorBusinessRules : BaseBusinessRules
{
    public async Task LogAuthorizationErrorShouldExistWhenSelected(X.LogAuthorizationError? item)
    {
        if (item == null)
            throw new BusinessException(LogAuthorizationErrorsBusinessMessages.LogAuthorizationErrorNotExists);
    }
}