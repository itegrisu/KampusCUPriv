using Application.Features.GeneralManagementFeatures.UserReminders.Commands.Create;
using Application.Features.GeneralManagementFeatures.UserReminders.Commands.Delete;
using Application.Features.GeneralManagementFeatures.UserReminders.Commands.Update;
using Application.Features.GeneralManagementFeatures.UserReminders.Commands.UploadFile;
using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByUserGid;
using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetList;
using Application.Features.OfferManagementFeatures.OfferFiles.Commands.UploadFile;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRemindersController : BaseController<CreateUserReminderCommand, DeleteUserReminderCommand, UpdateUserReminderCommand, GetByGidUserReminderQuery,
           CreatedUserReminderResponse, DeletedUserReminderResponse, UpdatedUserReminderResponse, GetByGidUserReminderResponse>
    {
        public UserRemindersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserReminderQuery getListUserReminderQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserReminderListItemDto> response = await Mediator.Send(getListUserReminderQuery);
            return Ok(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidListUserReminderQuery getByUserGidListUserReminderQuery)
        {
            GetListResponse<GetByUserGidListUserReminderListItemDto> response = await Mediator.Send(getByUserGidListUserReminderQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadUserReminderFile([FromBody] UploadUserReminderCommand uploadUserReminderCommand)
        {
            UploadUserReminderResponse response = await Mediator.Send(uploadUserReminderCommand);
            return Ok(response);
        }
    }
}
