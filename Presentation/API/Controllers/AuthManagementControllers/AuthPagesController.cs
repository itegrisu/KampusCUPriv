using API.Filters;
using Application.Features.AuthManagementFeatures.AuthPages.Commands.Create;
using Application.Features.AuthManagementFeatures.AuthPages.Commands.Delete;
using Application.Features.AuthManagementFeatures.AuthPages.Commands.Update;
using Application.Features.AuthManagementFeatures.AuthPages.Commands.UpdateRowNo;
using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByUserGid;
using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetList;
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
    public class AuthPagesController : BaseController<CreateAuthPageCommand, DeleteAuthPageCommand, UpdateAuthPageCommand, GetByGidAuthPageQuery,
     CreatedAuthPageResponse, DeletedAuthPageResponse, UpdatedAuthPageResponse, GetByGidAuthPageResponse>
    {
        public AuthPagesController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAuthPageQuery getListAuthPageQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListAuthPageListItemDto> response = await Mediator.Send(getListAuthPageQuery);
            return Ok(response);
        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> Sort([FromBody] UpdateRowNoAuthPageCommand command)
        {
            UpdateRowNoAuthPageResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> GetByGidPage([FromQuery] GetByUserGidAuthPageQuery getByGidAuthPageQuery)
        {
            GetListResponse<GetByUserGidAuthPageListItemDto> response = await Mediator.Send(getByGidAuthPageQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetByGidPageComboBox([FromQuery] GetByUserGidAuthPageQuery getByGidAuthPageQuery)
        {
            GetListResponse<GetByUserGidAuthPageListItemDto> response = await Mediator.Send(getByGidAuthPageQuery);
            return Ok(response);
        }
    }
}
