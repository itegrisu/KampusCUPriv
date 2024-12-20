using Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Create;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Update;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationDetailsController : BaseController<CreateReservationDetailCommand, DeleteReservationDetailCommand, UpdateReservationDetailCommand, GetByGidReservationDetailQuery,
         CreatedReservationDetailResponse, DeletedReservationDetailResponse, UpdatedReservationDetailResponse, GetByGidReservationDetailResponse>
    {
        public ReservationDetailsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListReservationDetailQuery getListReservationDetailQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListReservationDetailListItemDto> response = await Mediator.Send(getListReservationDetailQuery);
            return Ok(response);
        }


    }
}
