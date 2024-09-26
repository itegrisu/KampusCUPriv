using API.Filters;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Create;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Delete;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.LogOutLog;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Update;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetActiveLoginUser;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetLastTenLogin;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetList;
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
    public class LogSuccessedLoginsController : BaseController<CreateLogSuccessedLoginCommand, DeleteLogSuccessedLoginCommand, UpdateLogSuccessedLoginCommand, GetByGidLogSuccessedLoginQuery,
       CreatedLogSuccessedLoginResponse, DeletedLogSuccessedLoginResponse, UpdatedLogSuccessedLoginResponse, GetByGidLogSuccessedLoginResponse>
    {
        public LogSuccessedLoginsController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromBody] GetListLogSuccessedLoginQuery getListLogSuccessedLoginQuery)
        {
            GetListResponse<GetListLogSuccessedLoginListItemDto> response = await Mediator.Send(getListLogSuccessedLoginQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetActiveLogin([FromQuery] PageRequest pageRequest)
        {
            GetActiveLoginUserSuccessedLoginQuery getActiveLoginUserSuccessedLoginQuery = new GetActiveLoginUserSuccessedLoginQuery { PageRequest = pageRequest };
            GetListResponse<GetActiveLoginUserLogSuccessedLoginListItemDto> response = await Mediator.Send(getActiveLoginUserSuccessedLoginQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetLastTenLogin([FromQuery] GetLastTenLogSuccessedLoginQuery getLastTenLogSuccessedLoginQuery)
        {
            GetListResponse<GetLastTenLogSuccessedLoginListItemDto> response = await Mediator.Send(getLastTenLogSuccessedLoginQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> LogoutLog([FromBody] LogOutLogSuccessedLoginCommand logOutLogSuccessedLoginCommand)
        {
            LogOutLogSuccessedLoginResponse response = await Mediator.Send(logOutLogSuccessedLoginCommand);
            return Ok(response);
        }


    }

}
