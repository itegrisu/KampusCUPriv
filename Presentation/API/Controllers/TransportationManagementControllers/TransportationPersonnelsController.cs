using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetList;
using Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Queries.GetByServiceGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TransportationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationPersonnelsController : BaseController<CreateTransportationPersonnelCommand, DeleteTransportationPersonnelCommand, UpdateTransportationPersonnelCommand, GetByGidTransportationPersonnelQuery,
         CreatedTransportationPersonnelResponse, DeletedTransportationPersonnelResponse, UpdatedTransportationPersonnelResponse, GetByGidTransportationPersonnelResponse>
    {
        public TransportationPersonnelsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTransportationPersonnelQuery getListTransportationPersonnelQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTransportationPersonnelListItemDto> response = await Mediator.Send(getListTransportationPersonnelQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByServiceGid([FromQuery] GetByServiceGidListTransportationPersonnelQuery getByServiceGidListTransportationPersonnelQuery)
        {
            GetListResponse<GetByServiceGidListTransportationPersonnelListItemDto> response = await Mediator.Send(getByServiceGidListTransportationPersonnelQuery);
            return Ok(response);
        }


    }
}
