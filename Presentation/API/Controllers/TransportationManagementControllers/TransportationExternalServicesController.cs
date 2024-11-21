using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TransportationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationExternalServicesController : BaseController<CreateTransportationExternalServiceCommand, DeleteTransportationExternalServiceCommand, UpdateTransportationExternalServiceCommand, GetByGidTransportationExternalServiceQuery,
          CreatedTransportationExternalServiceResponse, DeletedTransportationExternalServiceResponse, UpdatedTransportationExternalServiceResponse, GetByGidTransportationExternalServiceResponse>
    {
        public TransportationExternalServicesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTransportationExternalServiceQuery getListTransportationExternalServiceQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTransportationExternalServiceListItemDto> response = await Mediator.Send(getListTransportationExternalServiceQuery);
            return Ok(response);
        }


    }
}
