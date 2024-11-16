using Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleInspections.Commands.UploadVehicleInspectionFile;
using Application.Features.VehicleManagementsFeatures.VehicleInspections.Queries.GetByVehicleGid;
using Application.Features.VehicleManagementsFeatures.VehicleInsurances.Commands.UploadVehicleInsuranceFile;
using Application.Features.VehicleManagementsFeatures.VehicleInsurances.Queries.GetByVehicleGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VehicleManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleInsurancesController : BaseController<CreateVehicleInsuranceCommand, DeleteVehicleInsuranceCommand, UpdateVehicleInsuranceCommand, GetByGidVehicleInsuranceQuery,
       CreatedVehicleInsuranceResponse, DeletedVehicleInsuranceResponse, UpdatedVehicleInsuranceResponse, GetByGidVehicleInsuranceResponse>
    {
        public VehicleInsurancesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVehicleInsuranceQuery getListVehicleInsuranceQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListVehicleInsuranceListItemDto> response = await Mediator.Send(getListVehicleInsuranceQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByVehicleGid([FromQuery] GetByVehicleGidListVehicleInsuranceQuery getByVehicleGidListVehicleInsuranceQuery)
        {
            GetListResponse<GetByVehicleGidListVehicleInsuranceListItemDto> response = await Mediator.Send(getByVehicleGidListVehicleInsuranceQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadVehicleInsuranceFile([FromBody] UploadVehicleInsuranceFileCommand uploadVehicleInsuranceFileCommand)
        {
            UploadVehicleInsuranceFileResponse response = await Mediator.Send(uploadVehicleInsuranceFileCommand);
            return Ok(response);
        }
    }
}
