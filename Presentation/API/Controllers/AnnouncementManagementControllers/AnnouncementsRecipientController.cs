using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Create;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Delete;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Update;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Queries.GetByGid;
using Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AnnouncementManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsRecipientController : BaseController<CreateAnnouncementRecipientCommand, DeleteAnnouncementRecipientCommand, UpdateAnnouncementRecipientCommand, GetByGidAnnouncementRecipientQuery,
        CreatedAnnouncementRecipientResponse, DeletedAnnouncementRecipientResponse, UpdatedAnnouncementRecipientResponse, GetByGidAnnouncementRecipientResponse>
    {
        public AnnouncementsRecipientController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAnnouncementRecipientQuery getListAnnouncementRecipientQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListAnnouncementRecipientListItemDto> response = await Mediator.Send(getListAnnouncementRecipientQuery);
            return Ok(response);
        }
    }
}
