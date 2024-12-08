using Application.Features.OfferManagementFeatures.OfferFiles.Commands.UploadFile;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetList;
using Application.Features.TransportationManagementsFeatures.Transportations.Queries.GetTransportationNo;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.CancelReport;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.CreateServiceWithGroup;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.ReportTransportationService;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.UpdateServiceWithGroup;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.UploadFile;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Queries.GetByTransportationGid;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Queries.GetServiceNo;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TransportationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationServicesController : BaseController<CreateTransportationServiceCommand, DeleteTransportationServiceCommand, UpdateTransportationServiceCommand, GetByGidTransportationServiceQuery,
       CreatedTransportationServiceResponse, DeletedTransportationServiceResponse, UpdatedTransportationServiceResponse, GetByGidTransportationServiceResponse>
    {
        public TransportationServicesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTransportationServiceQuery getListTransportationServiceQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTransportationServiceListItemDto> response = await Mediator.Send(getListTransportationServiceQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByTransportationGid([FromQuery] GetByTransportationGidListTransportationServiceQuery getByTransportationGidListTransportationServiceQuery)
        {
            GetListResponse<GetListTransportationServiceListItemDto> response = await Mediator.Send(getByTransportationGidListTransportationServiceQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadTransportationServiceFile([FromBody] UploadTransportationServiceFileCommand uploadTransportationServiceFileCommand)
        {
            UploadTransportationServiceFileResponse response = await Mediator.Send(uploadTransportationServiceFileCommand);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTransportationServiceWithGroup([FromBody] CreateTransportationServiceWithGroupCommand createTransportationServiceWithGroupCommand)
        {
            CreatedTransportationServiceWithGroupResponse response = await Mediator.Send(createTransportationServiceWithGroupCommand);
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateTransportationServiceWithGroup([FromBody] UpdateServiceWithGroupTransportationServiceCommand updateServiceWithGroupTransportationServiceCommand)
        {
            UpdateServiceWithGroupTransportationServiceResponse response = await Mediator.Send(updateServiceWithGroupTransportationServiceCommand);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetServiceNo([FromQuery] GetServiceNoQuery getServiceNoQuery)
        {
            GetServiceNoResponse response = await Mediator.Send(getServiceNoQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ReportTransportationService([FromBody] ReportTransportationServiceCommand reportTransportationServiceCommand)
        {
            ReportedTransportationServiceResponse response = await Mediator.Send(reportTransportationServiceCommand);
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CancelTransportationService([FromBody] CancelTransportationServiceCommand  cancelTransportationServiceCommand)
        {
            CancaledTransportationServiceResponse response = await Mediator.Send(cancelTransportationServiceCommand);
            return Ok(response);
        }
    }
}
