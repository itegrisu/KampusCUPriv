using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DefinitionManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationTypesController : BaseController<CreateOrganizationTypeCommand, DeleteOrganizationTypeCommand, UpdateOrganizationTypeCommand, GetByGidOrganizationTypeQuery,
       CreatedOrganizationTypeResponse, DeletedOrganizationTypeResponse, UpdatedOrganizationTypeResponse, GetByGidOrganizationTypeResponse>
    {
        public OrganizationTypesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOrganizationTypeQuery getListOrganizationTypeQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOrganizationTypeListItemDto> response = await Mediator.Send(getListOrganizationTypeQuery);
            return Ok(response);
        }


    }
}
