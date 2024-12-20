using Application.Features.AccommodationManagementFeatures.Reservations.Commands.Create;
using Application.Features.AccommodationManagementFeatures.Reservations.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.Reservations.Commands.Update;
using Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    public class HomeController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ReservationsController : BaseController<CreateReservationCommand, DeleteReservationCommand, UpdateReservationCommand, GetByGidReservationQuery,
         CreatedReservationResponse, DeletedReservationResponse, UpdatedReservationResponse, GetByGidReservationResponse>
        {
            public ReservationsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
            {
            }

            [HttpGet("[action]")]
            public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
            {
                GetListReservationQuery getListReservationQuery = new() { PageRequest = pageRequest };
                GetListResponse<GetListReservationListItemDto> response = await Mediator.Send(getListReservationQuery);
                return Ok(response);
            }


        }
    }
}
