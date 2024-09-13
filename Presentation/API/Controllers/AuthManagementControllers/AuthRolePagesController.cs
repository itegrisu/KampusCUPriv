using API.Filters;
using Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Create;
using Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Delete;
using Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Update;
using Application.Features.AuthManagementFeatures.AuthRolePages.Commands.UpdateRowNo;
using Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetList;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AuthManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthRolePagesController : BaseController<CreateAuthRolePageCommand, DeleteAuthRolePageCommand, UpdateAuthRolePageCommand, GetByIdAuthRolePageQuery,
        CreatedAuthRolePageResponse, DeletedAuthRolePageResponse, UpdatedAuthRolePageResponse, GetByGidAuthRolePageResponse>
    {
        public AuthRolePagesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> GetList([FromQuery] GetListAuthRolePageQuery getListAuthRolePageQuery)
        {
            GetListResponse<GetListAuthRolePageListItemDto> response = await Mediator.Send(getListAuthRolePageQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoAuthRolePageCommand command)
        {
            UpdateRowNoAuthRolePageResponse response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
