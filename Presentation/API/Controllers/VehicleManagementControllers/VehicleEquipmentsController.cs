using Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleDocuments.Commands.UploadDocumentFile;
using Application.Features.VehicleManagementsFeatures.VehicleDocuments.Commands.UploadVehicleDocumentFile;
using Application.Features.VehicleManagementsFeatures.VehicleDocuments.Queries.GetByVehicleGid;
using Application.Features.VehicleManagementsFeatures.VehicleEquipments.Commands.UploadVehicleEquipmentFile;
using Application.Features.VehicleManagementsFeatures.VehicleEquipments.Queries.GetByVehicleGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VehicleManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleEquipmentsController : BaseController<CreateVehicleEquipmentCommand, DeleteVehicleEquipmentCommand, UpdateVehicleEquipmentCommand, GetByGidVehicleEquipmentQuery,
        CreatedVehicleEquipmentResponse, DeletedVehicleEquipmentResponse, UpdatedVehicleEquipmentResponse, GetByGidVehicleEquipmentResponse>
    {
        public VehicleEquipmentsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVehicleEquipmentQuery getListVehicleEquipmentQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListVehicleEquipmentListItemDto> response = await Mediator.Send(getListVehicleEquipmentQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByVehicleGid([FromQuery] GetByVehicleGidListVehicleEquipmentQuery getByVehicleGidListVehicleEquipmentQuery)
        {
            GetListResponse<GetByVehicleGidListVehicleEquipmentListItemDto> response = await Mediator.Send(getByVehicleGidListVehicleEquipmentQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadVehicleEquipmentFile([FromBody] UploadVehicleEquipmentFileCommand uploadVehicleEquipmentFileCommand)
        {
            UploadVehicleEquipmentFileResponse response = await Mediator.Send(uploadVehicleEquipmentFileCommand);
            return Ok(response);
        }

    }
}
