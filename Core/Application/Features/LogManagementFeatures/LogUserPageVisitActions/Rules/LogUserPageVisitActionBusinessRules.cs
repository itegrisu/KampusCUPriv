using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Rules;

public class LogUserPageVisitActionBusinessRules : BaseBusinessRules
{
    public async Task LogUserPageVisitActionShouldExistWhenSelected(X.LogUserPageVisitAction? item)
    {
        if (item == null)
            throw new BusinessException(LogUserPageVisitActionsBusinessMessages.LogUserPageVisitActionNotExists);
    }
}