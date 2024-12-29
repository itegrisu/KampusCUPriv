using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Create;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Update;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGuestGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetList;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestAccommodationRoomsController : BaseController<CreateGuestAccommodationRoomCommand, DeleteGuestAccommodationRoomCommand, UpdateGuestAccommodationRoomCommand, GetByGidGuestAccommodationRoomQuery,
           CreatedGuestAccommodationRoomResponse, DeletedGuestAccommodationRoomResponse, UpdatedGuestAccommodationRoomResponse, GetByGidGuestAccommodationRoomResponse>
    {
        public GuestAccommodationRoomsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGuestAccommodationRoomQuery getListGuestAccommodationRoomQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListGuestAccommodationRoomListItemDto> response = await Mediator.Send(getListGuestAccommodationRoomQuery);
            return Ok(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetByGuestGid([FromQuery] GetByGuestGidListGuestAccommodationRoomQuery getByGuestGidListGuestAccommodationRoomQuery)
        {
            GetListResponse<GetByGuestGidListGuestAccommodationRoomListItemDto> response = await Mediator.Send(getByGuestGidListGuestAccommodationRoomQuery);
            return Ok(response);
        }
    }
}
