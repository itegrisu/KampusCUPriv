using API.Filters;
using Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Create;
using Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Delete;
using Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Update;
using Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LogManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogFailedLoginsController : BaseController<CreateLogFailedLoginCommand, DeleteLogFailedLoginCommand, UpdateLogFailedLoginCommand, GetByGidLogFailedLoginQuery,
        CreatedLogFailedLoginResponse, DeletedLogFailedLoginResponse, UpdatedLogFailedLoginResponse, GetByGidLogFailedLoginResponse>
    {
        public LogFailedLoginsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromBody] GetListLogFailedLoginQuery query)
        {
            GetListResponse<GetListLogFailedLoginListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }


    }
}
