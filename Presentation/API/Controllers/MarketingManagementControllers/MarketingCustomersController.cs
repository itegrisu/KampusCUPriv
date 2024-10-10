using Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Create;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Delete;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Update;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetByGid;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.MarketingManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingCustomersController : BaseController<CreateMarketingCustomerCommand, DeleteMarketingCustomerCommand, UpdateMarketingCustomerCommand, GetByGidMarketingCustomerQuery,
        CreatedMarketingCustomerResponse, DeletedMarketingCustomerResponse, UpdatedMarketingCustomerResponse, GetByGidMarketingCustomerResponse>
    {
        public MarketingCustomersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListMarketingCustomerQuery getListMarketingCustomerQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListMarketingCustomerListItemDto> response = await Mediator.Send(getListMarketingCustomerQuery);
            return Ok(response);
        }


    }
}
