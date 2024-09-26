using API.Filters;
using Application.Features.TaskManagementFeatures.TaskUsers.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskUsers.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskUsers.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByUserGid;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetList;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetListByTaskGid;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetTaskCountList;
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
    public class TaskUsersController : BaseController<CreateTaskUserCommand, DeleteTaskUserCommand, UpdateTaskUserCommand, GetByGidTaskUserQuery,
    CreatedTaskUserResponse, DeletedTaskUserResponse, UpdatedTaskUserResponse, GetByGidTaskUserResponse>
    {
        public TaskUsersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTaskUserQuery getListTaskUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTaskUserListItemDto> response = await Mediator.Send(getListTaskUserQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetListByTaskGid([FromQuery] GetListByTaskGidTaskUserQuery query)
        {
            GetListResponse<GetListByTaskGidTaskUserListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetByUserGid([FromQuery] GetByUserGidTaskUserQuery request)
        {
            GetListResponse<GetByUserGidTaskUserResponse> response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetTaskCountList([FromQuery] GetTaskCountListTaskUserQuery request)
        {
            GetListResponse<GetTaskCountListTaskUserListItemDto> response = await Mediator.Send(request);

            return Ok(response);
        }


    }

}
