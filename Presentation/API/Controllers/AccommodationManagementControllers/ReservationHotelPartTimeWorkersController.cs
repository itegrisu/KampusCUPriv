using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Create;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Update;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHotelPartTimeWorkersController : BaseController<CreateReservationHotelPartTimeWorkerCommand, DeleteReservationHotelPartTimeWorkerCommand, UpdateReservationHotelPartTimeWorkerCommand, GetByGidReservationHotelPartTimeWorkerQuery,
        CreatedReservationHotelPartTimeWorkerResponse, DeletedReservationHotelPartTimeWorkerResponse, UpdatedReservationHotelPartTimeWorkerResponse, GetByGidReservationHotelPartTimeWorkerResponse>
    {
        public ReservationHotelPartTimeWorkersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] GetListReservationHotelPartTimeWorkerQuery getListReservationHotelPartTimeWorkerQuery)
        {
            GetListResponse<GetListReservationHotelPartTimeWorkerListItemDto> response = await Mediator.Send(getListReservationHotelPartTimeWorkerQuery);
            return Ok(response);
        }


    }
}
