using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleInsurances.Commands.UploadVehicleInsuranceFile;
using Application.Features.VehicleManagementsFeatures.VehicleInsurances.Queries.GetByVehicleGid;
using Application.Features.VehicleManagementsFeatures.VehicleMaintenances.Commands.UploadVehicleMaintenanceFile;
using Application.Features.VehicleManagementsFeatures.VehicleMaintenances.Queries.GetByVehicleGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VehicleManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMaintenancesController : BaseController<CreateVehicleMaintenanceCommand, DeleteVehicleMaintenanceCommand, UpdateVehicleMaintenanceCommand, GetByGidVehicleMaintenanceQuery,
        CreatedVehicleMaintenanceResponse, DeletedVehicleMaintenanceResponse, UpdatedVehicleMaintenanceResponse, GetByGidVehicleMaintenanceResponse>
    {
        public VehicleMaintenancesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVehicleMaintenanceQuery getListVehicleMaintenanceQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListVehicleMaintenanceListItemDto> response = await Mediator.Send(getListVehicleMaintenanceQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByVehicleGid([FromQuery] GetByVehicleGidListVehicleMaintenanceQuery getByVehicleGidListVehicleMaintenanceQuery)
        {
            GetListResponse<GetByVehicleGidListVehicleMaintenanceListItemDto> response = await Mediator.Send(getByVehicleGidListVehicleMaintenanceQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadVehicleMaintenanceFile([FromBody] UploadVehicleMaintenanceFileCommand uploadVehicleMaintenanceFileCommand)
        {
            UploadVehicleMaintenanceFileResponse response = await Mediator.Send(uploadVehicleMaintenanceFileCommand);
            return Ok(response);
        }
    }
}
