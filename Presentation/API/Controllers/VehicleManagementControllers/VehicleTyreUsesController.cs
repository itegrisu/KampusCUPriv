using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleInsurances.Queries.GetByVehicleGid;
using Application.Features.VehicleManagementsFeatures.VehicleTyreUses.Queries.GetByVehicleGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VehicleManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTyreUsesController : BaseController<CreateVehicleTyreUseCommand, DeleteVehicleTyreUseCommand, UpdateVehicleTyreUseCommand, GetByGidVehicleTyreUseQuery,
            CreatedVehicleTyreUseResponse, DeletedVehicleTyreUseResponse, UpdatedVehicleTyreUseResponse, GetByGidVehicleTyreUseResponse>
    {
        public VehicleTyreUsesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVehicleTyreUseQuery getListVehicleTyreUseQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListVehicleTyreUseListItemDto> response = await Mediator.Send(getListVehicleTyreUseQuery);
            return Ok(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetByVehicleGid([FromQuery] GetByVehicleGidListVehicleTyreUseQuery getByVehicleGidListVehicleTyreUseQuery)
        {
            GetListResponse<GetByVehicleGidListVehicleTyreUseListItemDto> response = await Mediator.Send(getByVehicleGidListVehicleTyreUseQuery);
            return Ok(response);
        }
    }
}
