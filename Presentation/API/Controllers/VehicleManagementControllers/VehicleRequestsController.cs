using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByUserGid;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleRequests.Queries.GetByUserGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VehicleManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleRequestsController : BaseController<CreateVehicleRequestCommand, DeleteVehicleRequestCommand, UpdateVehicleRequestCommand, GetByGidVehicleRequestQuery,
        CreatedVehicleRequestResponse, DeletedVehicleRequestResponse, UpdatedVehicleRequestResponse, GetByGidVehicleRequestResponse>
    {
        public VehicleRequestsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVehicleRequestQuery getListVehicleRequestQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListVehicleRequestListItemDto> response = await Mediator.Send(getListVehicleRequestQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidListVehicleRequestQuery getByUserGidListVehicleRequestQuery)
        {
            GetListResponse<GetByUserGidListVehicleRequestListItemDto> response = await Mediator.Send(getByUserGidListVehicleRequestQuery);
            return Ok(response);
        }
    }
}
