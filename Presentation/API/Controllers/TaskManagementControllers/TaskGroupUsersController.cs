using API.Filters;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGroupGid;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TaskManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskGroupUsersController : BaseController<CreateTaskGroupUserCommand, DeleteTaskGroupUserCommand, UpdateTaskGroupUserCommand, GetByGidTaskGroupUserQuery,
    CreatedTaskGroupUserResponse, DeletedTaskGroupUserResponse, UpdatedTaskGroupUserResponse, GetByGidTaskGroupUserResponse>
    {
        public TaskGroupUsersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTaskGroupUserQuery getListTaskGroupUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTaskGroupUserListItemDto> response = await Mediator.Send(getListTaskGroupUserQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetByGroupGid([FromQuery] GetByGroupGidTaskGroupUserQuery getByGroupGidTaskGroupUserQuery)
        {
            GetListResponse<GetByGroupGidTaskGroupUserResponse> response = await Mediator.Send(getByGroupGidTaskGroupUserQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetByGroupGidComboBox([FromQuery] GetByGroupGidTaskGroupUserQuery getByGroupGidTaskGroupUserQuery)
        {
            GetListResponse<GetByGroupGidTaskGroupUserResponse> response = await Mediator.Send(getByGroupGidTaskGroupUserQuery);
            return Ok(response);
        }
    }

}
