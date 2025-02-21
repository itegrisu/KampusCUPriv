using Application.Features.CommunicationFeatures.Announcements.Commands.Create;
using Application.Features.CommunicationFeatures.Announcements.Commands.Delete;
using Application.Features.CommunicationFeatures.Announcements.Commands.Update;
using Application.Features.CommunicationFeatures.Announcements.Queries.GetByGid;
using Application.Features.CommunicationFeatures.Announcements.Queries.GetList;
using Application.Features.CommunicationManagementFeatures.Announcements.Queries.GetByClubGid;
using Application.Features.CommunicationManagementFeatures.Events.Queries.GetByClubGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CommunicationManagementControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : BaseController<CreateAnnouncementCommand, DeleteAnnouncementCommand, UpdateAnnouncementCommand, GetByGidAnnouncementQuery,
        CreatedAnnouncementResponse, DeletedAnnouncementResponse, UpdatedAnnouncementResponse, GetByGidAnnouncementResponse>
    {
        public AnnouncementsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAnnouncementQuery getListAnnouncementQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListAnnouncementListItemDto> response = await Mediator.Send(getListAnnouncementQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByClubGid([FromQuery] GetByClubGidListAnnouncementQuery getByClubGidListAnnouncementQuery)
        {
            GetListResponse<GetByClubGidListAnnouncementListItemDto> response = await Mediator.Send(getByClubGidListAnnouncementQuery);
            return Ok(response);
        }

    }
}
