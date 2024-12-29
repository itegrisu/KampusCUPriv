using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Create;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Update;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGuestGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetList;
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
    public class GuestAccommodationPersonsController : BaseController<CreateGuestAccommodationPersonCommand, DeleteGuestAccommodationPersonCommand, UpdateGuestAccommodationPersonCommand, GetByGidGuestAccommodationPersonQuery,
        CreatedGuestAccommodationPersonResponse, DeletedGuestAccommodationPersonResponse, UpdatedGuestAccommodationPersonResponse, GetByGidGuestAccommodationPersonResponse>
    {
        public GuestAccommodationPersonsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGuestAccommodationPersonQuery getListGuestAccommodationPersonQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListGuestAccommodationPersonListItemDto> response = await Mediator.Send(getListGuestAccommodationPersonQuery);
            return Ok(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetByGuestGid([FromQuery] GetByGuestGidListGuestAccommodationPersonQuery getByGuestGidListGuestAccommodationPersonQuery)
        {
            GetListResponse<GetByGuestGidListGuestAccommodationPersonListItemDto> response = await Mediator.Send(getByGuestGidListGuestAccommodationPersonQuery);
            return Ok(response);
        }
    }
}
