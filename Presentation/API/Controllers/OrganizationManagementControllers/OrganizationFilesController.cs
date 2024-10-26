using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Update;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.UpdateRowNo;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.UploadFile;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByOrganizationGid;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrganizationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationFilesController : BaseController<CreateOrganizationFileCommand, DeleteOrganizationFileCommand, UpdateOrganizationFileCommand, GetByGidOrganizationFileQuery,
         CreatedOrganizationFileResponse, DeletedOrganizationFileResponse, UpdatedOrganizationFileResponse, GetByGidOrganizationFileResponse>
    {
        public OrganizationFilesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOrganizationFileQuery getListOrganizationFileQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOrganizationFileListItemDto> response = await Mediator.Send(getListOrganizationFileQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoOrganizationFileCommand command)
        {
            UpdateRowNoOrganizationFileResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByOrganizationGid([FromQuery] GetByOrganizationGidListOrganizationFileQuery getByOrganizationGidListOrganizationFileQuery)
        {
            GetListResponse<GetByOrganizationGidListOrganizationFileListItemDto> response = await Mediator.Send(getByOrganizationGidListOrganizationFileQuery);
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UploadOrganizationFile([FromBody] UploadOrganizationFileCommand uploadOrganizationFileCommand)
        {
            UploadOrganizationFileResponse response = await Mediator.Send(uploadOrganizationFileCommand);
            return Ok(response);
        }

    }
}
