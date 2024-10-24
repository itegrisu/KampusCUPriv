using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Update;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.UpdateRowNo;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByOrganizationGid;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrganizationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationGroupsController : BaseController<CreateOrganizationGroupCommand, DeleteOrganizationGroupCommand, UpdateOrganizationGroupCommand, GetByGidOrganizationGroupQuery,
        CreatedOrganizationGroupResponse, DeletedOrganizationGroupResponse, UpdatedOrganizationGroupResponse, GetByGidOrganizationGroupResponse>
    {
        public OrganizationGroupsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOrganizationGroupQuery getListOrganizationGroupQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOrganizationGroupListItemDto> response = await Mediator.Send(getListOrganizationGroupQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoOrganizationGroupCommand command)
        {
            UpdateRowNoOrganizationGroupResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByOrganizationGid([FromQuery] GetByOrganizationGidListOrganizationGroupQuery getByOrganizationGidListOrganizationGroupQuery)
        {
            GetListResponse<GetByOrganizationGidListOrganizationGroupListItemDto> response = await Mediator.Send(getByOrganizationGidListOrganizationGroupQuery);
            return Ok(response);
        }
    }
}
