using Application.Features.CommunicationFeatures.Events.Commands.Create;
using Application.Features.CommunicationFeatures.Events.Commands.Delete;
using Application.Features.CommunicationFeatures.Events.Commands.Update;
using Application.Features.CommunicationFeatures.Events.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Events.Queries.GetList;
using Application.Features.CommunicationManagementFeatures.Events.Queries.GetByClubGid;
using Application.Features.CommunicationManagementFeatures.Events.Queries.GetByCount;
using Application.Features.CommunicationManagementFeatures.Events.Queries.GetByUserGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CommunicationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : BaseController<CreateEventCommand, DeleteEventCommand, UpdateEventCommand, GetByGidEventQuery,
      CreatedEventResponse, DeletedEventResponse, UpdatedEventResponse, GetByGidEventResponse>
    {
        public EventsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListEventQuery getListEventQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListEventListItemDto> response = await Mediator.Send(getListEventQuery);
            return Ok(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidListEventQuery getByUserGidListEventQuery)
        {
            GetListResponse<GetByUserGidListEventListItemDto> response = await Mediator.Send(getByUserGidListEventQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByCount([FromQuery] GetByCountListEventQuery getByCountListEventQuery)
        {
            GetListResponse<GetByCountListEventListItemDto> response = await Mediator.Send(getByCountListEventQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByClubGid([FromQuery] GetByClubGidListEventQuery getByClubGidListEventQuery)
        {
            GetListResponse<GetByClubGidListEventListItemDto> response = await Mediator.Send(getByClubGidListEventQuery);
            return Ok(response);
        }
    }
}
