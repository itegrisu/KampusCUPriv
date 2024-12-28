using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Create;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Update;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHotelStaffsController : BaseController<CreateReservationHotelStaffCommand, DeleteReservationHotelStaffCommand, UpdateReservationHotelStaffCommand, GetByGidReservationHotelStaffQuery,
        CreatedReservationHotelStaffResponse, DeletedReservationHotelStaffResponse, UpdatedReservationHotelStaffResponse, GetByGidReservationHotelStaffResponse>
    {
        public ReservationHotelStaffsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] GetListReservationHotelStaffQuery getListReservationHotelStaffQuery)
        {
            GetListResponse<GetListReservationHotelStaffListItemDto> response = await Mediator.Send(getListReservationHotelStaffQuery);
            return Ok(response);
        }


    }
}
