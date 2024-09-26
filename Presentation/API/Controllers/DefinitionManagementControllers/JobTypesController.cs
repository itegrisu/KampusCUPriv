using Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypesController : BaseController<CreateJobTypeCommand, DeleteJobTypeCommand, UpdateJobTypeCommand, GetByGidJobTypeQuery,
         CreatedJobTypeResponse, DeletedJobTypeResponse, UpdatedJobTypeResponse, GetByGidJobTypeResponse>
    {
        public JobTypesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListJobTypeQuery getListJobTypeQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListJobTypeListItemDto> response = await Mediator.Send(getListJobTypeQuery);
            return Ok(response);
        }


    }
}
