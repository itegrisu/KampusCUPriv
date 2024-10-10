using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Rules;

public class MarketingVisitPlanBusinessRules : BaseBusinessRules
{
    public async Task MarketingVisitPlanShouldExistWhenSelected(X.MarketingVisitPlan? item)
    {
        if (item == null)
            throw new BusinessException(MarketingVisitPlansBusinessMessages.MarketingVisitPlanNotExists);
    }
}