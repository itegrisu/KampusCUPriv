using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Create;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Update;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestAccommodationResultsController : BaseController<CreateGuestAccommodationResultCommand, DeleteGuestAccommodationResultCommand, UpdateGuestAccommodationResultCommand, GetByGidGuestAccommodationResultQuery,
        CreatedGuestAccommodationResultResponse, DeletedGuestAccommodationResultResponse, UpdatedGuestAccommodationResultResponse, GetByGidGuestAccommodationResultResponse>
    {
        public GuestAccommodationResultsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGuestAccommodationResultQuery getListGuestAccommodationResultQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListGuestAccommodationResultListItemDto> response = await Mediator.Send(getListGuestAccommodationResultQuery);
            return Ok(response);
        }
    }
}
