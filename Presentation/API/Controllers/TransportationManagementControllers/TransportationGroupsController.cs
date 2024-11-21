using Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TransportationManagementControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TransportationGroupsController : BaseController<CreateTransportationGroupCommand, DeleteTransportationGroupCommand, UpdateTransportationGroupCommand, GetByGidTransportationGroupQuery,
        CreatedTransportationGroupResponse, DeletedTransportationGroupResponse, UpdatedTransportationGroupResponse, GetByGidTransportationGroupResponse>
    {
        public TransportationGroupsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTransportationGroupQuery getListTransportationGroupQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTransportationGroupListItemDto> response = await Mediator.Send(getListTransportationGroupQuery);
            return Ok(response);
        }


    }
}
