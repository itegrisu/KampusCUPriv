using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid;
using Application.Features.TransportationManagementFeatures.Transportations.Commands.Create;
using Application.Features.TransportationManagementFeatures.Transportations.Commands.Delete;
using Application.Features.TransportationManagementFeatures.Transportations.Commands.Update;
using Application.Features.TransportationManagementFeatures.Transportations.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.Transportations.Queries.GetList;
using Application.Features.TransportationManagementsFeatures.Transportations.Queries.GetTransportationNo;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TransportationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationsController : BaseController<CreateTransportationCommand, DeleteTransportationCommand, UpdateTransportationCommand, GetByGidTransportationQuery,
         CreatedTransportationResponse, DeletedTransportationResponse, UpdatedTransportationResponse, GetByGidTransportationResponse>
    {
        public TransportationsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTransportationQuery getListTransportationQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTransportationListItemDto> response = await Mediator.Send(getListTransportationQuery);
            return Ok(response);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetTransportationNo([FromQuery] GetTransportationNoQuery getTransportationNoQuery)
        {
            GetTransportationNoResponse response = await Mediator.Send(getTransportationNoQuery);
            return Ok(response);
        }
    }
}
