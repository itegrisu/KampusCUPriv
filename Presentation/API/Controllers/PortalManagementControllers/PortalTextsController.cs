using API.Filters;
using Application.Features.PortalManagementFeatures.PortalTexts.Commands.Create;
using Application.Features.PortalManagementFeatures.PortalTexts.Commands.Delete;
using Application.Features.PortalManagementFeatures.PortalTexts.Commands.Update;
using Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetByGid;
using Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PortalManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortalTextsController : BaseController<CreatePortalTextCommand, DeletePortalTextCommand, UpdatePortalTextCommand, GetByGidPortalTextQuery,
           CreatedPortalTextResponse, DeletedPortalTextResponse, UpdatedPortalTextResponse, GetByGidPortalTextResponse>
    {
        public PortalTextsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPortalTextQuery getListPortalTextQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPortalTextListItemDto> response = await Mediator.Send(getListPortalTextQuery);
            return Ok(response);
        }


    }
}
