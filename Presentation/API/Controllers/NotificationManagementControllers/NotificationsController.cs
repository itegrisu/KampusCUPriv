using API.Filters;
using Application.Features.NotificationManagementFeatures.Notifications.Commands.Create;
using Application.Features.NotificationManagementFeatures.Notifications.Commands.Delete;
using Application.Features.NotificationManagementFeatures.Notifications.Commands.MarkTheReadAllNotification;
using Application.Features.NotificationManagementFeatures.Notifications.Commands.MarkTheReadByGid;
using Application.Features.NotificationManagementFeatures.Notifications.Commands.Update;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByUserGid;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetList;
using Application.Features.NotificationManagementFeatures.Notifications.Queries.GetUnreadByUserGid;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.NotificationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : BaseController<CreateNotificationCommand, DeleteNotificationCommand, UpdateNotificationCommand, GetByGidNotificationQuery,
        CreatedNotificationResponse, DeletedNotificationResponse, UpdatedNotificationResponse, GetByGidNotificationResponse>
    {
        public NotificationsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> GetList([FromBody] GetListNotificationQuery request)
        {
            GetListResponse<GetListNotificationListItemDto> response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> GetByUserGid([FromBody] GetByUserGidQuery request)
        {
            GetListResponse<GetByUserGidNotificationListItemDto> response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> GetUnreadByUserGid([FromQuery] GetUnreadByUserGidQuery request)
        {
            GetListResponse<GetUnreadByUserGidListItemDto> response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> MarkTheReadAllNotification([FromQuery] MarkTheReadAllNotificationCommand request)
        {
            MarkTheReadAllNotificationResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> MarkTheReadByGid([FromQuery] MarkTheReadByGidCommand request)
        {
            MarkTheReadByGidResponse response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
