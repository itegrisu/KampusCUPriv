using API.Filters;
using Application.Features.SupportManagementFeatures.SupportRequests.Commands.Create;
using Application.Features.SupportManagementFeatures.SupportRequests.Commands.Delete;
using Application.Features.SupportManagementFeatures.SupportRequests.Commands.Update;
using Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SupportManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportRequestsController : BaseController<CreateSupportRequestCommand, DeleteSupportRequestCommand, UpdateSupportRequestCommand, GetByGidSupportRequestQuery,
        CreatedSupportRequestResponse, DeletedSupportRequestResponse, UpdatedSupportRequestResponse, GetByGidSupportRequestResponse>
    {
        public SupportRequestsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromBody] GetListSupportRequestQuery getListSupportRequestQuery)
        {
            GetListResponse<GetListSupportRequestListItemDto> response = await Mediator.Send(getListSupportRequestQuery);
            return Ok(response);
        }

    }

}
