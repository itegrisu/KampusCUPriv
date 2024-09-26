using Application.Features.DefinationManagementFeatures.Warehouses.Commands.Create;
using Application.Features.DefinationManagementFeatures.Warehouses.Commands.Delete;
using Application.Features.DefinationManagementFeatures.Warehouses.Commands.Update;
using Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetByGid;
using Application.Features.DefinationManagementFeatures.Warehouses.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {


        protected readonly IMediator Mediator;
        protected readonly clsAuth _clsAuth;

        public WarehousesController(IMediator mediator, clsAuth clsAuth)
        {
            Mediator = mediator;
            _clsAuth = clsAuth;
        }



        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListWarehouseQuery getListWarehouseQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListWarehouseListItemDto> response = await Mediator.Send(getListWarehouseQuery);
            return Ok(response);
        }

        //crud islemleri *******************************************************************************************

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public virtual async Task<IActionResult> Add([FromBody] CreateWarehouseCommand request)
        {
            CreatedWarehouseResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public virtual async Task<IActionResult> Update([FromBody] UpdateWarehouseCommand request)
        {
            UpdatedWarehouseResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{Gid}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] DeleteWarehouseCommand deleteCommand)
        {
            DeletedWarehouseResponse response = await Mediator.Send(deleteCommand);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetByGid([FromQuery] GetByGidWarehouseQuery getByIdQuery)
        {
            GetByGidWarehouseResponse response = await Mediator.Send(getByIdQuery);
            return Ok(response);
        }


    }
}
