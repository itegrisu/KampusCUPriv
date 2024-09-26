using API.Filters;
using Application.Features.LogManagementFeatures.LogEmailSends.Commands.Create;
using Application.Features.LogManagementFeatures.LogEmailSends.Commands.Delete;
using Application.Features.LogManagementFeatures.LogEmailSends.Commands.Update;
using Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetList;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LogManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogEmailSendsController : BaseController<CreateLogEmailSendCommand, DeleteLogEmailSendCommand, UpdateLogEmailSendCommand, GetByGidLogEmailSendQuery,
         CreatedLogEmailSendResponse, DeletedLogEmailSendResponse, UpdatedLogEmailSendResponse, GetByGidLogEmailSendResponse>
    {
        public LogEmailSendsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListLogEmailSendQuery getListLogEmailSendQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListLogEmailSendListItemDto> response = await Mediator.Send(getListLogEmailSendQuery);
            return Ok(response);
        }
    }
}
