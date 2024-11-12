using API.Controllers;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Create;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Delete;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Update;
//using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.UpdateRowNo;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FAFV1.API.Controllers.OrganizationManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationItemMessagesController : BaseController<CreateOrganizationItemMessageCommand, DeleteOrganizationItemMessageCommand, UpdateOrganizationItemMessageCommand, GetByGidOrganizationItemMessageQuery,
        CreatedOrganizationItemMessageResponse, DeletedOrganizationItemMessageResponse, UpdatedOrganizationItemMessageResponse, GetByGidOrganizationItemMessageResponse>
    {
        public OrganizationItemMessagesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetList([FromQuery] GetListOrganizationItemMessageQuery getListOrganizationItemMessageQuery)
        {
            GetListResponse<GetListOrganizationItemMessageListItemDto> response = await Mediator.Send(getListOrganizationItemMessageQuery);
            return Ok(response);
        }
    }
}