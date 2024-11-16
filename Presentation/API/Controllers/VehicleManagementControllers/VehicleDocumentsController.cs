using Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleDocuments.Commands.UploadDocumentFile;
using Application.Features.VehicleManagementsFeatures.VehicleDocuments.Commands.UploadVehicleDocumentFile;
using Application.Features.VehicleManagementsFeatures.VehicleDocuments.Queries.GetByVehicleGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VehicleManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleDocumentsController : BaseController<CreateVehicleDocumentCommand, DeleteVehicleDocumentCommand, UpdateVehicleDocumentCommand, GetByGidVehicleDocumentQuery,
       CreatedVehicleDocumentResponse, DeletedVehicleDocumentResponse, UpdatedVehicleDocumentResponse, GetByGidVehicleDocumentResponse>
    {
        public VehicleDocumentsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVehicleDocumentQuery getListVehicleDocumentQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListVehicleDocumentListItemDto> response = await Mediator.Send(getListVehicleDocumentQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByVehicleGid([FromQuery] GetByVehicleGidListVehicleDocumentQuery getByVehicleGidListVehicleDocumentQuery)
        {
            GetListResponse<GetByVehicleGidListVehicleDocumentListItemDto> response = await Mediator.Send(getByVehicleGidListVehicleDocumentQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadVehicleDocumentFile ([FromBody] UploadVehicleDocumentFileCommand uploadVehicleDocumentFileCommand)
        {
            UploadVehicleDocumentFileResponse response = await Mediator.Send(uploadVehicleDocumentFileCommand);
            return Ok(response);
        }
    }
}
