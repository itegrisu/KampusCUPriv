using Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Create;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Update;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationDatesController : BaseController<CreateAccommodationDateCommand, DeleteAccommodationDateCommand, UpdateAccommodationDateCommand, GetByGidAccommodationDateQuery,
        CreatedAccommodationDateResponse, DeletedAccommodationDateResponse, UpdatedAccommodationDateResponse, GetByGidAccommodationDateResponse>
    {
        public AccommodationDatesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] GetListAccommodationDateQuery getListAccommodationDateQuery)
        {
            GetListResponse<GetListAccommodationDateListItemDto> response = await Mediator.Send(getListAccommodationDateQuery);
            return Ok(response);
        }


    }
}
