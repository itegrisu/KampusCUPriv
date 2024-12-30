using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Create;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Update;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AccommodationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartTimeWorkersController : BaseController<CreatePartTimeWorkerCommand, DeletePartTimeWorkerCommand, UpdatePartTimeWorkerCommand, GetByGidPartTimeWorkerQuery,
        CreatedPartTimeWorkerResponse, DeletedPartTimeWorkerResponse, UpdatedPartTimeWorkerResponse, GetByGidPartTimeWorkerResponse>
    {
        public PartTimeWorkersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPartTimeWorkerQuery getListPartTimeWorkerQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPartTimeWorkerListItemDto> response = await Mediator.Send(getListPartTimeWorkerQuery);
            return Ok(response);
        }


    }
}
