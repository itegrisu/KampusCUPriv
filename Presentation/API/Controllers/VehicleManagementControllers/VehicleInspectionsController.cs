using Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleInspections.Commands.UploadVehicleInspectionFile;
using Application.Features.VehicleManagementsFeatures.VehicleInspections.Queries.GetByVehicleGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VehicleManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleInspectionsController : BaseController<CreateVehicleInspectionCommand, DeleteVehicleInspectionCommand, UpdateVehicleInspectionCommand, GetByGidVehicleInspectionQuery,
          CreatedVehicleInspectionResponse, DeletedVehicleInspectionResponse, UpdatedVehicleInspectionResponse, GetByGidVehicleInspectionResponse>
    {
        public VehicleInspectionsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVehicleInspectionQuery getListVehicleInspectionQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListVehicleInspectionListItemDto> response = await Mediator.Send(getListVehicleInspectionQuery);
            return Ok(response);
        }

        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByVehicleGid([FromQuery] GetByVehicleGidListVehicleInspectionQuery getByVehicleGidListVehicleInspectionQuery)
        {
            GetListResponse<GetByVehicleGidListVehicleInspectionListItemDto> response = await Mediator.Send(getByVehicleGidListVehicleInspectionQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadVehicleInspectionFile([FromBody] UploadVehicleInspectionFileCommand uploadVehicleInspectionFileCommand)
        {
            UploadVehicleInspectionFileResponse response = await Mediator.Send(uploadVehicleInspectionFileCommand);
            return Ok(response);
        }

    }
}
