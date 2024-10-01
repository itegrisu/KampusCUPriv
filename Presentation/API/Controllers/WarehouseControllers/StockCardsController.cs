using Application.Features.WarehouseManagementFeatures.StockCards.Commands.Create;
using Application.Features.WarehouseManagementFeatures.StockCards.Commands.Delete;
using Application.Features.WarehouseManagementFeatures.StockCards.Commands.Update;
using Application.Features.WarehouseManagementFeatures.StockCards.Queries.GetByGid;
using Application.Features.WarehouseManagementFeatures.StockCards.Queries.GetList;

using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.WarehouseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockCardsController : ControllerBase
    {

        protected readonly IMediator Mediator;
        protected readonly clsAuth _clsAuth;

        public StockCardsController(IMediator mediator, clsAuth clsAuth)
        {
            Mediator = mediator;
            _clsAuth = clsAuth;
        }


        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListStockCardQuery getListStockCardQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListStockCardListItemDto> response = await Mediator.Send(getListStockCardQuery);
            return Ok(response);
        }

        //Crud işlemleri ***********************************************************************************************************

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public virtual async Task<IActionResult> Add([FromBody] CreateStockCardCommand request)
        {
            CreatedStockCardResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public virtual async Task<IActionResult> Update([FromBody] UpdateStockCardCommand request)
        {
            UpdatedStockCardResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{Gid}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] DeleteStockCardCommand deleteCommand)
        {
            DeletedStockCardResponse response = await Mediator.Send(deleteCommand);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetByGid([FromQuery] GetByGidStockCardQuery getByIdQuery)
        {
            GetByGidStockCardResponse response = await Mediator.Send(getByIdQuery);
            return Ok(response);
        }



    }
}
