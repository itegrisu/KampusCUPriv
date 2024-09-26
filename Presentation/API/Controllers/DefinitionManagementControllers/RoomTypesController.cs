using Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : BaseController<CreateRoomTypeCommand, DeleteRoomTypeCommand, UpdateRoomTypeCommand, GetByGidRoomTypeQuery,
         CreatedRoomTypeResponse, DeletedRoomTypeResponse, UpdatedRoomTypeResponse, GetByGidRoomTypeResponse>
    {
        public RoomTypesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListRoomTypeQuery getListRoomTypeQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListRoomTypeListItemDto> response = await Mediator.Send(getListRoomTypeQuery);
            return Ok(response);
        }


    }
}
