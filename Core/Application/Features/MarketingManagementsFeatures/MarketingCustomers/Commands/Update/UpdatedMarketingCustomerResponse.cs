using Application.Features.Base;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Update;

public class UpdatedMarketingCustomerResponse : BaseResponse, IResponse
{
    public GetByGidMarketingCustomerResponse Obj { get; set; }
}