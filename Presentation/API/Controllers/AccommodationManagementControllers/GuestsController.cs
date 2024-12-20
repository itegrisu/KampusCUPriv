using Application.Features.AccommodationManagementFeatures.Guests.Commands.Create;
using Application.Features.AccommodationManagementFeatures.Guests.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.Guests.Commands.Update;
using Application.Features.AccommodationManagementFeatures.Guests.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.Guests.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : BaseController<CreateGuestCommand, DeleteGuestCommand, UpdateGuestCommand, GetByGidGuestQuery,
       CreatedGuestResponse, DeletedGuestResponse, UpdatedGuestResponse, GetByGidGuestResponse>
    {
        public GuestsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGuestQuery getListGuestQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListGuestListItemDto> response = await Mediator.Send(getListGuestQuery);
            return Ok(response);
        }


    }
}
