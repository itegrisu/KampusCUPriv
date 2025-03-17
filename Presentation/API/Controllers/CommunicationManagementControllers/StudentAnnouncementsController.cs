using Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Create;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Delete;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Update;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetByGid;
using Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetList;
using Application.Features.CommunicationManagementFeatures.Events.Queries.GetByUserGid;
using Application.Features.CommunicationManagementFeatures.StudentAnnouncements.Commands.MarkAllAsRead;
using Application.Features.CommunicationManagementFeatures.StudentAnnouncements.Queries.GetByUserGid;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CommunicationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAnnouncementsController : BaseController<CreateStudentAnnouncementCommand, DeleteStudentAnnouncementCommand, UpdateStudentAnnouncementCommand, GetByGidStudentAnnouncementQuery,
            CreatedStudentAnnouncementResponse, DeletedStudentAnnouncementResponse, UpdatedStudentAnnouncementResponse, GetByGidStudentAnnouncementResponse>
    {
        public StudentAnnouncementsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListStudentAnnouncementQuery getListStudentAnnouncementQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListStudentAnnouncementListItemDto> response = await Mediator.Send(getListStudentAnnouncementQuery);
            return Ok(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidListStudentAnnouncementQuery getByUserGidListStudentAnnouncementQuery)
        {
            GetListResponse<GetByUserGidListStudentAnnouncementListItemDto> response = await Mediator.Send(getByUserGidListStudentAnnouncementQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> MarkAllAsRead([FromBody] MarkAllAsReadStudentAnnouncementCommand markAllAsReadStudentAnnouncementCommand)
        {
            MarkAllAsReadStudentAnnouncementResponse response = await Mediator.Send(markAllAsReadStudentAnnouncementCommand);
            return Ok(response);
        }
    }
}
