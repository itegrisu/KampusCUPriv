using Application.Features.GeneralFeatures.Admins.Commands.Create;
using Application.Features.GeneralFeatures.Admins.Commands.Delete;
using Application.Features.GeneralFeatures.Admins.Commands.Update;
using Application.Features.GeneralFeatures.Admins.Queries.GetByGid;
using Application.Features.GeneralFeatures.Admins.Queries.GetList;
using Application.Features.GeneralManagementFeatures.Admins.Commands.Login;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Application.Features.GeneralManagementFeatures.Users.Commands.RefreshToken;
using Application.Features.GeneralManagementFeatures.Users.Commands.RevokeRefreshToken;
using Application.Helpers;
using Application.Repositories.GeneralManagementRepo.AdminRepo;
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
    public class AdminsController : BaseController<CreateAdminCommand, DeleteAdminCommand, UpdateAdminCommand, GetByGidAdminQuery,
      CreatedAdminResponse, DeletedAdminResponse, UpdatedAdminResponse, GetByGidAdminResponse>
    {
        private readonly IAdminReadRepository _adminReadRepository;
        private readonly IAdminWriteRepository _adminWriteRepository;
        public AdminsController(IMediator mediator, clsAuth clsAuth, IAdminWriteRepository adminWriteRepository, IAdminReadRepository adminReadRepository) : base(mediator, clsAuth)
        {
            _adminWriteRepository = adminWriteRepository;
            _adminReadRepository = adminReadRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAdminQuery getListAdminQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListAdminListItemDto> response = await Mediator.Send(getListAdminQuery);
            return Ok(response);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginAdminCommand loginAdminCommand)
        {
            LoginAdminResponse response = await Mediator.Send(loginAdminCommand);
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
