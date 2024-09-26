using API.Filters;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Create;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.CreateByCheckBox;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Delete;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Update;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.UpdateRowNo;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetList;
using Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetListByUserGid;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AuthManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserRolesController : BaseController<CreateAuthUserRoleCommand, DeleteAuthUserRoleCommand, UpdateAuthUserRoleCommand, GetByGidAuthUserRoleQuery,
        CreatedAuthUserRoleResponse, DeletedAuthUserRoleResponse, UpdatedAuthUserRoleResponse, GetByGidAuthUserRoleResponse>
    {
        public AuthUserRolesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAuthUserRoleQuery getListAuthUserRoleQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListAuthUserRoleListItemDto> response = await Mediator.Send(getListAuthUserRoleQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> GetListByUserGid([FromQuery] GetListByUserGidAuthUserRoleQuery getListByUserGidAuthUserRoleQuery)
        {
            GetListResponse<GetListByUserGidAuthUserRoleListItemDto> response = await Mediator.Send(getListByUserGidAuthUserRoleQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoAuthUserRoleCommand command)
        {
            UpdateRowNoAuthUserRoleResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        
        public async Task<IActionResult> AddByCheckBox([FromBody] CreateByCheckBoxAuthUserRoleCommand command)
        {
            CreatedByCheckBoxAuthUserRoleResponse response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
