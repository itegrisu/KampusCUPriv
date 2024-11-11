using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.UploadFile;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByUserGid;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleTransactions.Commands.UploadContractFile;
using Application.Features.VehicleManagementsFeatures.VehicleTransactions.Commands.UploadLicenseFile;
using Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetByVehicleTransactionGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VehicleManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTransactionsController : BaseController<CreateVehicleTransactionCommand, DeleteVehicleTransactionCommand, UpdateVehicleTransactionCommand, GetByGidVehicleTransactionQuery,
         CreatedVehicleTransactionResponse, DeletedVehicleTransactionResponse, UpdatedVehicleTransactionResponse, GetByGidVehicleTransactionResponse>
    {
        public VehicleTransactionsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVehicleTransactionQuery getListVehicleTransactionQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListVehicleTransactionListItemDto> response = await Mediator.Send(getListVehicleTransactionQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByVehicleTransactionGid([FromQuery] GetByVehicleTransactionGidListVehicleTransactionQuery getByVehicleTransactionGidListVehicleTransactionQuery)
        {
            GetListResponse<GetByVehicleTransactionGidListVehicleTransactionListItemDto> response = await Mediator.Send(getByVehicleTransactionGidListVehicleTransactionQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadVehicleTransactionContractFile([FromBody] UploadVehicleTransactionContractFileCommand uploadVehicleTransactionContractFileCommand)
        {
            UploadVehicleTransactionContractFileResponse response = await Mediator.Send(uploadVehicleTransactionContractFileCommand);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadVehicleTransactionLicenseFile([FromBody] UploadVehicleTransactionLicenseFileCommand uploadVehicleTransactionLicenseFileCommand)
        {
            UploadVehicleTransactionLicenseFileResponse response = await Mediator.Send(uploadVehicleTransactionLicenseFileCommand);
            return Ok(response);
        }
    }
}
