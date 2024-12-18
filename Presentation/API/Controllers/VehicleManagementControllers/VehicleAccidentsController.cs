using Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleAccidents.Commands.UploadVehicleAccidentFile;
using Application.Features.VehicleManagementsFeatures.VehicleAccidents.Commands.UploadVehicleAccidentImageFile;
using Application.Features.VehicleManagementsFeatures.VehicleAccidents.Queries.GetByUserGid;
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
    public class VehicleAccidentsController : BaseController<CreateVehicleAccidentCommand, DeleteVehicleAccidentCommand, UpdateVehicleAccidentCommand, GetByGidVehicleAccidentQuery,
        CreatedVehicleAccidentResponse, DeletedVehicleAccidentResponse, UpdatedVehicleAccidentResponse, GetByGidVehicleAccidentResponse>
    {
        public VehicleAccidentsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVehicleAccidentQuery getListVehicleAccidentQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListVehicleAccidentListItemDto> response = await Mediator.Send(getListVehicleAccidentQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByVehicleGid([FromQuery] GetByVehicleGidListVehicleAccidentQuery getByVehicleGidListVehicleAccidentQuery)
        {
            GetListResponse<GetByVehicleGidListVehicleAccidentListItemDto> response = await Mediator.Send(getByVehicleGidListVehicleAccidentQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadVehicleAccidentFile([FromBody] UploadVehicleAccidentFileCommand uploadVehicleAccidentFileCommand)
        {
            UploadVehicleAccidentFileResponse response = await Mediator.Send(uploadVehicleAccidentFileCommand);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadVehicleAccidentImageFile([FromBody] UploadVehicleAccidentImageFileCommand uploadVehicleAccidentImageFileCommand)
        {
            UploadVehicleAccidentImageFileResponse response = await Mediator.Send(uploadVehicleAccidentImageFileCommand);
            return Ok(response);
        }
    }
}
