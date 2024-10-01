using Application.Features.OrganizationManagementFeatures.Organizations.Commands.Create;
using Application.Features.OrganizationManagementFeatures.Organizations.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.Organizations.Commands.Update;
using Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrganizationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : BaseController<CreateOrganizationCommand, DeleteOrganizationCommand, UpdateOrganizationCommand, GetByGidOrganizationQuery,
         CreatedOrganizationResponse, DeletedOrganizationResponse, UpdatedOrganizationResponse, GetByGidOrganizationResponse>
    {
        public OrganizationsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOrganizationQuery getListOrganizationQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOrganizationListItemDto> response = await Mediator.Send(getListOrganizationQuery);
            return Ok(response);
        }


    }
}
