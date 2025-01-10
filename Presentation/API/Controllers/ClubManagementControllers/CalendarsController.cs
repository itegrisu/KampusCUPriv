using Application.Features.CommunicationFeatures.Calendars.Commands.Create;
using Application.Features.CommunicationFeatures.Calendars.Commands.Delete;
using Application.Features.CommunicationFeatures.Calendars.Commands.Update;
using Application.Features.CommunicationFeatures.Calendars.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Calendars.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ClubManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarsController : BaseController<CreateCalendarCommand, DeleteCalendarCommand, UpdateCalendarCommand, GetByGidCalendarQuery,
          CreatedCalendarResponse, DeletedCalendarResponse, UpdatedCalendarResponse, GetByGidCalendarResponse>
    {
        public CalendarsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCalendarQuery getListCalendarQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListCalendarListItemDto> response = await Mediator.Send(getListCalendarQuery);
            return Ok(response);
        }


    }
}
