using Application.Features.LogManagementFeatures.LogUserPageVisits.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Rules;

public class LogUserPageVisitBusinessRules : BaseBusinessRules
{
    public async Task LogUserPageVisitShouldExistWhenSelected(X.LogUserPageVisit? item)
    {
        if (item == null)
            throw new BusinessException(LogUserPageVisitsBusinessMessages.LogUserPageVisitNotExists);
    }
}