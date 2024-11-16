using Application.Features.VehicleManagementFeatures.Tyres.Commands.Create;
using Application.Features.VehicleManagementFeatures.Tyres.Commands.Delete;
using Application.Features.VehicleManagementFeatures.Tyres.Commands.Update;
using Application.Features.VehicleManagementFeatures.Tyres.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.Tyres.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VehicleManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TyresController : BaseController<CreateTyreCommand, DeleteTyreCommand, UpdateTyreCommand, GetByGidTyreQuery,
        CreatedTyreResponse, DeletedTyreResponse, UpdatedTyreResponse, GetByGidTyreResponse>
    {
        public TyresController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTyreQuery getListTyreQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTyreListItemDto> response = await Mediator.Send(getListTyreQuery);
            return Ok(response);
        }


    }
}
