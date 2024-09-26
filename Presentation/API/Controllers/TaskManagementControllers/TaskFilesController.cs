using API.Filters;
using Application.Features.TaskManagementFeatures.TaskFiles.Commands.Create;
using Application.Features.TaskManagementFeatures.TaskFiles.Commands.Delete;
using Application.Features.TaskManagementFeatures.TaskFiles.Commands.Update;
using Application.Features.TaskManagementFeatures.TaskFiles.Commands.UploadTaskFile;
using Application.Features.TaskManagementFeatures.TaskFiles.Commands.UploadTaskFileTemp;
using Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetList;
using Application.Features.TaskManagementFeatures.TaskFiles.Queries.GetListByTaskGid;
using Application.Features.TaskManagementFeatures.Tasks.Queries.GetByGid;
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
    public class TaskFilesController : BaseController<CreateTaskFileCommand, DeleteTaskFileCommand, UpdateTaskFileCommand, GetByGidTaskQuery,
        CreatedTaskFileResponse, DeletedTaskFileResponse, UpdatedTaskFileResponse, GetByGidTaskResponse>
    {
        public TaskFilesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTaskFileQuery getListTaskFileQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTaskFileListItemDto> response = await Mediator.Send(getListTaskFileQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetListByTaskGid([FromQuery] GetListByTaskGidTaskFileQuery request)
        {
            GetListResponse<GetListByTaskGidTaskFileListItemDto> response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> UploadTaskFile([FromBody] UploadTaskFileCommand uploadTaskFileCommand)
        {
            UploadTaskFileResponse response = await Mediator.Send(uploadTaskFileCommand);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> UploadTemp([FromQuery] Guid gid)
        {
            UploadTaskFileTempCommand uploadTaskFileTempCommand = new()
            {
                Params = gid,
                FormFiles = Request.Form.Files
            };
            UploadTaskFileTempResponse response = await Mediator.Send(uploadTaskFileTempCommand);
            return Ok(response);
        }


    }
}
