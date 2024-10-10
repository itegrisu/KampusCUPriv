using Application.Features.Base;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Update;

public class UpdatedMarketingVisitPlanResponse : BaseResponse, IResponse
{
    public GetByGidMarketingVisitPlanResponse Obj { get; set; }
}