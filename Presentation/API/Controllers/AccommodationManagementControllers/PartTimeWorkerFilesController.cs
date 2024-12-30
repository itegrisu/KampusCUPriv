using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Create;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Update;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.UploadWorkerFile;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetList;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.UploadDocumentFile;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartTimeWorkerFilesController : BaseController<CreatePartTimeWorkerFileCommand, DeletePartTimeWorkerFileCommand, UpdatePartTimeWorkerFileCommand, GetByGidPartTimeWorkerFileQuery,
        CreatedPartTimeWorkerFileResponse, DeletedPartTimeWorkerFileResponse, UpdatedPartTimeWorkerFileResponse, GetByGidPartTimeWorkerFileResponse>
    {
        public PartTimeWorkerFilesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] GetListPartTimeWorkerFileQuery getListPartTimeWorkerFileQuery)
        {
            GetListResponse<GetListPartTimeWorkerFileListItemDto> response = await Mediator.Send(getListPartTimeWorkerFileQuery);
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UploadWorkerFile([FromBody] UploadWorkerFileCommand uploadWorkerFileCommand)
        {
            UploadWorkerFileResponse response = await Mediator.Send(uploadWorkerFileCommand);
            return Ok(response);
        }

    }
}
