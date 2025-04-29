using Application.Features.GeneralFeatures.Users.Commands.Create;
using Application.Features.GeneralFeatures.Users.Commands.Delete;
using Application.Features.GeneralFeatures.Users.Commands.Update;
using Application.Features.GeneralFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralFeatures.Users.Queries.GetList;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Application.Features.GeneralManagementFeatures.Users.Commands.RefreshToken;
using Application.Features.GeneralManagementFeatures.Users.Commands.RevokeRefreshToken;
using Application.Features.GeneralManagementFeatures.Users.Commands.VerifyEmail;
using Application.Helpers;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Core.Application.Request;
using Core.Application.Responses;
using Infrastracture.Helpers.cls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<CreateUserCommand, DeleteUserCommand, UpdateUserCommand, GetByGidUserQuery,
        CreatedUserResponse, DeletedUserResponse, UpdatedUserResponse, GetByGidUserResponse>
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        public UsersController(IMediator mediator, clsAuth clsAuth, IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository) : base(mediator, clsAuth)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserListItemDto> response = await Mediator.Send(getListUserQuery);
            return Ok(response);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            LoginUserResponse response = await Mediator.Send(loginUserCommand);
            return Ok(response);
        }

        [HttpPost("VerifyEmailUser")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmailUser([FromBody] VerifyEmailUserCommand verifyEmailUserCommand)
        {
            VerifyEmailUserResponse response = await Mediator.Send(verifyEmailUserCommand);
            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand refreshTokenCommand)
        {
            RefreshTokenResponse response = await Mediator.Send(refreshTokenCommand);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeRefreshTokenCommand revokeRefreshTokenCommand)
        {
            RevokeRefreshTokenResponse response = await Mediator.Send(revokeRefreshTokenCommand);
            return Ok(response);
        }

    }
}
