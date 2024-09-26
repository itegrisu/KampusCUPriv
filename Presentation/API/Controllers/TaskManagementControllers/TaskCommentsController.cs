using API.Filters;
using Application.Features.TaskManagementFeatures.TaskComments.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskComments.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskComments.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByGid;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByTaskGid;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetByTaskUserGid;
using Application.Features.TaskManagementFeatures.TaskComments.Queries.GetList;
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
    public class TaskCommentsController : BaseController<CreateTaskCommentCommand, DeleteTaskCommentCommand, UpdateTaskCommentCommand, GetByGidTaskCommentQuery,
        CreatedTaskCommentResponse, DeletedTaskCommentResponse, UpdatedTaskCommentResponse, GetByGidTaskCommentResponse>
    {
        public TaskCommentsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTaskCommentQuery getListTaskCommentQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTaskCommentListItemDto> response = await Mediator.Send(getListTaskCommentQuery);
            return Ok(response);
        }


        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetByTaskUserGidTaskComment([FromQuery] GetByTaskUserGidTaskCommentQuery request)
        {
            GetListResponse<GetByTaskUserGidTaskCommentResponse> response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetByTaskGidTaskComment([FromQuery] GetByTaskGidTaskCommentQuery request)
        {
            GetListResponse<GetByTaskGidTaskCommentResponse> response = await Mediator.Send(request);
            return Ok(response);
        }

    }
}