using Application.Features.Base;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Create;

public class CreatedMarketingCustomerResponse : BaseResponse, IResponse
{
    public GetByGidMarketingCustomerResponse Obj { get; set; }
}