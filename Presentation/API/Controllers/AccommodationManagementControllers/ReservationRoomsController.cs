using Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Create;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Update;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationRoomsController : BaseController<CreateReservationRoomCommand, DeleteReservationRoomCommand, UpdateReservationRoomCommand, GetByGidReservationRoomQuery,
        CreatedReservationRoomResponse, DeletedReservationRoomResponse, UpdatedReservationRoomResponse, GetByGidReservationRoomResponse>
    {
        public ReservationRoomsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] GetListReservationRoomQuery getListReservationRoomQuery)
        {
            GetListResponse<GetListReservationRoomListItemDto> response = await Mediator.Send(getListReservationRoomQuery);
            return Ok(response);
        }


    }
}
