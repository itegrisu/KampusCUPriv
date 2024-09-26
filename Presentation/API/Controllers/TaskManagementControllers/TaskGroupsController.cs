using API.Filters;
using Application.Features.TaskManagementFeatures.TaskGroups.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskGroups.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskGroups.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetList;
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
    public class TaskGroupsController : BaseController<CreateTaskGroupCommand, DeleteTaskGroupCommand, UpdateTaskGroupCommand, GetByGidTaskGroupQuery,
    CreatedTaskGroupResponse, DeletedTaskGroupResponse, UpdatedTaskGroupResponse, GetByGidTaskGroupResponse>
    {
        public TaskGroupsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTaskGroupQuery getListTaskGroupQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTaskGroupListItemDto> response = await Mediator.Send(getListTaskGroupQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetListComboBox([FromQuery] PageRequest pageRequest)
        {
            GetListTaskGroupQuery getListTaskGroupQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTaskGroupListItemDto> response = await Mediator.Send(getListTaskGroupQuery);
            return Ok(response);
        }
    }

}
