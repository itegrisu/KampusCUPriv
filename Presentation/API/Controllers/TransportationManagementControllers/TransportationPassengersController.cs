using Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetList;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetList;
using Application.Features.TransportationManagementsFeatures.TransportationPassengers.Queries.GetByGroupGid;
using Application.Features.TransportationManagementsFeatures.TransportationPassengers.Queries.GetByServiceGid;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Queries.GetByTransportationGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.TransportationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationPassengersController : BaseController<CreateTransportationPassengerCommand, DeleteTransportationPassengerCommand, UpdateTransportationPassengerCommand, GetByGidTransportationPassengerQuery,
         CreatedTransportationPassengerResponse, DeletedTransportationPassengerResponse, UpdatedTransportationPassengerResponse, GetByGidTransportationPassengerResponse>
    {
        public TransportationPassengersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTransportationPassengerQuery getListTransportationPassengerQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTransportationPassengerListItemDto> response = await Mediator.Send(getListTransportationPassengerQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByGroupGid([FromQuery] GetByGroupGidListTransportationPassengerQuery getByGroupGidListTransportationPassengerQuery)
        {
            GetListResponse<GetByGroupGidListTransportationPassengerListItemDto> response = await Mediator.Send(getByGroupGidListTransportationPassengerQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByServiceGid([FromQuery] GetByServiceGidListTransportationPassengerQuery getByServiceGidListTransportationPassengerQuery)
        {
            GetListResponse<GetByServiceGidListTransportationPassengerListItemDto> response = await Mediator.Send(getByServiceGidListTransportationPassengerQuery);
            return Ok(response);
        }


    }
}
