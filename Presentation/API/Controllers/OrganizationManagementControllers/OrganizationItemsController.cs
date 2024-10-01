using Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Update;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.UpdateRowNo;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetList;
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
    public class OrganizationItemsController : BaseController<CreateOrganizationItemCommand, DeleteOrganizationItemCommand, UpdateOrganizationItemCommand, GetByGidOrganizationItemQuery,
       CreatedOrganizationItemResponse, DeletedOrganizationItemResponse, UpdatedOrganizationItemResponse, GetByGidOrganizationItemResponse>
    {
        public OrganizationItemsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOrganizationItemQuery getListOrganizationItemQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListOrganizationItemListItemDto> response = await Mediator.Send(getListOrganizationItemQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoOrganizationItemCommand command)
        {
            UpdateRowNoOrganizationItemResponse response = await Mediator.Send(command);
            return Ok(response);
        }

    }
}
