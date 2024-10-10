using Application.Features.Base;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Create;

public class CreatedMarketingVisitPlanResponse : BaseResponse, IResponse
{
    public GetByGidMarketingVisitPlanResponse Obj { get; set; }
}