using API.Filters;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Create;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Delete;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Update;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SupportManagementControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SupportMessageDetailsController : BaseController<CreateSupportMessageDetailCommand, DeleteSupportMessageDetailCommand, UpdateSupportMessageDetailCommand, GetByGidSupportMessageDetailQuery,
       CreatedSupportMessageDetailResponse, DeletedSupportMessageDetailResponse, UpdatedSupportMessageDetailResponse, GetByGidSupportMessageDetailResponse>
    {
        public SupportMessageDetailsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSupportMessageDetailQuery getListSupportMessageDetailQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListSupportMessageDetailListItemDto> response = await Mediator.Send(getListSupportMessageDetailQuery);
            return Ok(response);
        }


    }

}
