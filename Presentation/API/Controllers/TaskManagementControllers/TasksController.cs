using API.Filters;
using Application.Features.TaskManagementFeatures.Tasks.Commands.Archive;
using Application.Features.TaskManagementFeatures.Tasks.Commands.Create;
using Application.Features.TaskManagementFeatures.Tasks.Commands.Delete;
using Application.Features.TaskManagementFeatures.Tasks.Commands.Update;
using Application.Features.TaskManagementFeatures.Tasks.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.Tasks.Queries.GetList;
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
    public class TasksController : BaseController<CreateTaskCommand, DeleteTaskCommand, UpdateTaskCommand, GetByGidTaskQuery,
        CreatedTaskResponse, DeletedTaskResponse, UpdatedTaskResponse, GetByGidTaskResponse>
    {
        public TasksController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTaskQuery getListTaskQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTaskListItemDto> response = await Mediator.Send(getListTaskQuery);
            return Ok(response);
        }

        [HttpDelete("[action]/{Gid}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> Archive([FromRoute] ArchivedTaskCommand archiveTaskCommand)
        {
            ArchivedTaskResponse response = await Mediator.Send(archiveTaskCommand);
            return Ok(response);
        }

    }
}
