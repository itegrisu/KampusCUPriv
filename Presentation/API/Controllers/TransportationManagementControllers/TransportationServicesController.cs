using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetList;
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
    }
}
