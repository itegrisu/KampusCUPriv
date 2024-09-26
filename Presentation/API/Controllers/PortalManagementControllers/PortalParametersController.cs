using Application.Features.PortalManagementFeatures.PortalParameters.Commands.Create;
using Application.Features.PortalManagementFeatures.PortalParameters.Commands.Delete;
using Application.Features.PortalManagementFeatures.PortalParameters.Commands.Update;
using Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetByGid;
using Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetList;
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
    public class PortalParametersController : BaseController<CreatePortalParameterCommand, DeletePortalParameterCommand, UpdatePortalParameterCommand, GetByGidPortalParameterQuery,
            CreatedPortalParameterResponse, DeletedPortalParameterResponse, UpdatedPortalParameterResponse, GetByGidPortalParameterResponse>
    {
        public PortalParametersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPortalParameterQuery getListPortalParameterQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListPortalParameterListItemDto> response = await Mediator.Send(getListPortalParameterQuery);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByGidComboBox([FromQuery] GetByGidPortalParameterQuery getByIdQuery)
        {
            GetByGidPortalParameterResponse response = await Mediator.Send(getByIdQuery);
            return Ok(response);
        }

    }
}
