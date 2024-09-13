using API.Filters;
using Application.Features.AuthManagementFeatures.AuthRoles.Commands.Create;
using Application.Features.AuthManagementFeatures.AuthRoles.Commands.Delete;
using Application.Features.AuthManagementFeatures.AuthRoles.Commands.Update;
using Application.Features.AuthManagementFeatures.AuthRoles.Commands.UpdateRowNo;
using Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetList;
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
    public class AuthRolesController : BaseController<CreateAuthRoleCommand, DeleteAuthRoleCommand, UpdateAuthRoleCommand, GetByGidAuthRoleQuery,
    CreatedAuthRoleResponse, DeletedAuthRoleResponse, UpdatedAuthRoleResponse, GetByGidAuthRoleResponse>
    {
        public AuthRolesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAuthRoleQuery getListAuthRoleQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListAuthRoleListItemDto> response = await Mediator.Send(getListAuthRoleQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoAuthRoleCommand command)
        {
            UpdateRowNoAuthRoleResponse response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
