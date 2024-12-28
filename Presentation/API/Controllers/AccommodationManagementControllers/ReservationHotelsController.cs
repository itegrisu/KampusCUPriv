using Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Create;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Update;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHotelsController : BaseController<CreateReservationHotelCommand, DeleteReservationHotelCommand, UpdateReservationHotelCommand, GetByGidReservationHotelQuery,
       CreatedReservationHotelResponse, DeletedReservationHotelResponse, UpdatedReservationHotelResponse, GetByGidReservationHotelResponse>
    {
        public ReservationHotelsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] GetListReservationHotelQuery getListReservationHotelQuery)
        {
            GetListResponse<GetListReservationHotelListItemDto> response = await Mediator.Send(getListReservationHotelQuery);
            return Ok(response);
        }


    }
}
