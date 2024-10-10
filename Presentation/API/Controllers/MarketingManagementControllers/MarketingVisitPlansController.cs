using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Create;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Delete;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Update;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Queries.GetByGid;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Queries.GetList;
using Application.Features.MarketingManagementsFeatures.MarketingVisitPlans.Queries.GetByUserGid;
using Application.Features.MarketingManagementsFeatures.MarketingVisitPlans.Queries.GetByYearList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.MarketingManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingVisitPlansController : BaseController<CreateMarketingVisitPlanCommand, DeleteMarketingVisitPlanCommand, UpdateMarketingVisitPlanCommand, GetByGidMarketingVisitPlanQuery,
     CreatedMarketingVisitPlanResponse, DeletedMarketingVisitPlanResponse, UpdatedMarketingVisitPlanResponse, GetByGidMarketingVisitPlanResponse>
    {
        public MarketingVisitPlansController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListMarketingVisitPlanQuery getListMarketingVisitPlanQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListMarketingVisitPlanListItemDto> response = await Mediator.Send(getListMarketingVisitPlanQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByYearList([FromQuery] GetByYearListMarketingVisitPlanQuery getByYearListMarketingVisitPlanQuery)
        {
            GetListResponse<GetByYearListMarketingVisitPlanListItemDto> response = await Mediator.Send(getByYearListMarketingVisitPlanQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGidList([FromQuery] GetByUserGidListMarketingVisitPlanQuery getByUserGidListMarketingVisitPlanQuery)
        {
            GetListResponse<GetByUserGidListMarketingVisitPlanListItemDto> response = await Mediator.Send(getByUserGidListMarketingVisitPlanQuery);
            return Ok(response);
        }

    }
}
