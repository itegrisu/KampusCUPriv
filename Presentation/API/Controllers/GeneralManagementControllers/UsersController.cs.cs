using Application.Features.GeneralFeatures.Users.Commands.Create;
using Application.Features.GeneralFeatures.Users.Commands.Delete;
using Application.Features.GeneralFeatures.Users.Commands.Update;
using Application.Features.GeneralFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralFeatures.Users.Queries.GetList;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<CreateUserCommand, DeleteUserCommand, UpdateUserCommand, GetByGidUserQuery,
        CreatedUserResponse, DeletedUserResponse, UpdatedUserResponse, GetByGidUserResponse>
    {
        public UsersController(IMediator mediator, clsAuth clsAuth) : base(mediator, clsAuth)
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserListItemDto> response = await Mediator.Send(getListUserQuery);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            LoginUserResponse response = await Mediator.Send(loginUserCommand);
            return Ok(response);
        }
    }
}
