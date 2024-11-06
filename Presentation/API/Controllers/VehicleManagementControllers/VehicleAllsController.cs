using Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VehicleManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAllsController : BaseController<CreateVehicleAllCommand, DeleteVehicleAllCommand, UpdateVehicleAllCommand, GetByGidVehicleAllQuery,
         CreatedVehicleAllResponse, DeletedVehicleAllResponse, UpdatedVehicleAllResponse, GetByGidVehicleAllResponse>
    {
        public VehicleAllsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVehicleAllQuery getListVehicleAllQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListVehicleAllListItemDto> response = await Mediator.Send(getListVehicleAllQuery);
            return Ok(response);
        }


    }
}
