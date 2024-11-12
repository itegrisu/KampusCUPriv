using API.Controllers;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Update;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.UpdateRowNo;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.UploadDocument;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetByOrganizationGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FAFV1.API.Controllers.OrganizationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationItemFilesController : BaseController<CreateOrganizationItemFileCommand, DeleteOrganizationItemFileCommand, UpdateOrganizationItemFileCommand, GetByGidOrganizationItemFileQuery,
        CreatedOrganizationItemFileResponse, DeletedOrganizationItemFileResponse, UpdatedOrganizationItemFileResponse, GetByGidOrganizationItemFileResponse>
    {
        public OrganizationItemFilesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOrganizationItemFileQuery getListOrganizationItemFileQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOrganizationItemFileListItemDto> response = await Mediator.Send(getListOrganizationItemFileQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoOrganizationItemFileCommand command)
        {
            UpdateRowNoOrganizationItemFileResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetByOrganizationGid([FromQuery] GetByOrganizationGidListOrganizationItemFileQuery getByOrganizationGidListOrganizationFileQuery)
        {
            GetListResponse<GetByOrganizationGidListOrganizationItemFileListItemDto> response = await Mediator.Send(getByOrganizationGidListOrganizationFileQuery);
            return Ok(response);
        }


        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UploadOrganizationItemFile([FromBody] UploadDocumentOrganizationItemFileCommand uploadOrganizationFileCommand)
        {
            UploadDocumentOrganizationItemFileResponse response = await Mediator.Send(uploadOrganizationFileCommand);
            return Ok(response);
        }

    }
}