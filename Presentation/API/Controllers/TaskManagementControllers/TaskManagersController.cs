using API.Filters;
using Application.Features.TaskManagementFeatures.TaskManagers.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskManagers.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskManagers.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetList;
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
    public class TaskManagersController : BaseController<CreateTaskManagerCommand, DeleteTaskManagerCommand, UpdateTaskManagerCommand, GetByGidTaskManagerQuery,
    CreatedTaskManagerResponse, DeletedTaskManagerResponse, UpdatedTaskManagerResponse, GetByGidTaskManagerResponse>
    {
        public TaskManagersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTaskManagerQuery getListTaskManagerQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTaskManagerListItemDto> response = await Mediator.Send(getListTaskManagerQuery);
            return Ok(response);
        }
    }

}
