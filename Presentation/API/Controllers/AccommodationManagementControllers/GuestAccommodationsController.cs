using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Create;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Update;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GuestAccommodationsController : BaseController<CreateGuestAccommodationCommand, DeleteGuestAccommodationCommand, UpdateGuestAccommodationCommand, GetByGidGuestAccommodationQuery,
        CreatedGuestAccommodationResponse, DeletedGuestAccommodationResponse, UpdatedGuestAccommodationResponse, GetByGidGuestAccommodationResponse>
    {
        public GuestAccommodationsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGuestAccommodationQuery getListGuestAccommodationQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListGuestAccommodationListItemDto> response = await Mediator.Send(getListGuestAccommodationQuery);
            return Ok(response);
        }

    }
}
