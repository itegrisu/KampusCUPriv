using Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Create;
using Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Delete;
using Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Update;
using Application.Features.AnnouncementManagementFeatures.Announcements.Commands.UpdateRowNo;
using Application.Features.AnnouncementManagementFeatures.Announcements.Commands.UploadAnnouncementFile;
using Application.Features.AnnouncementManagementFeatures.Announcements.Commands.UploadAnnouncementFileTemp;
using Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetByGid;
using Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetList;
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
    public class AnnouncementsController : BaseController<CreateAnnouncementCommand, DeleteAnnouncementCommand, UpdateAnnouncementCommand, GetByGidAnnouncementQuery,
        CreatedAnnouncementResponse, DeletedAnnouncementResponse, UpdatedAnnouncementResponse, GetByGidAnnouncementResponse>
    {
        public AnnouncementsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]


        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAnnouncementQuery getListAnnouncementQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListAnnouncementListItemDto> response = await Mediator.Send(getListAnnouncementQuery);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> UploadAnnouncementFile([FromBody] UploadAnnouncementFileCommand uploadAnnouncementFileCommand)
        {
            UploadAnnouncementFileResponse response = await Mediator.Send(uploadAnnouncementFileCommand);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> UploadAnnouncementFileTemp([FromQuery] string Gid)
        {
            UploadAnnouncementTempCommand uploadAnnouncementFileTempCommand = new()  //
            {
                Params = Gid,
                FormFiles = Request.Form.Files
            };
            UploadAnnouncementTempResponse response = await Mediator.Send(uploadAnnouncementFileTempCommand);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Sort([FromBody] UpdateRowNoAnnouncementCommand command)
        {
            UpdateRowNoAnnouncementResponse response = await Mediator.Send(command);
            return Ok(response);
        }

    }
}
