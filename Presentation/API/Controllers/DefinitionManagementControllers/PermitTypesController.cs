using Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitTypesController : BaseController<CreatePermitTypeCommand, DeletePermitTypeCommand, UpdatePermitTypeCommand, GetByGidPermitTypeQuery,
       CreatedPermitTypeResponse, DeletedPermitTypeResponse, UpdatedPermitTypeResponse, GetByGidPermitTypeResponse>
    {
        public PermitTypesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPermitTypeQuery getListPermitTypeQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPermitTypeListItemDto> response = await Mediator.Send(getListPermitTypeQuery);
            return Ok(response);
        }


    }
}
