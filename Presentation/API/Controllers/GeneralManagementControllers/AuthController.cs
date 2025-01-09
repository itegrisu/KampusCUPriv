using Application.Abstractions;
using Application.Features.GeneralManagementFeatures.Auth.Commands.DeleteRefreshToken;
using Application.Features.GeneralManagementFeatures.Auth.Commands.GetTokenByRefreshToken;
using Application.Features.GeneralManagementFeatures.Auth.Commands.Login;
using Application.Features.GeneralManagementFeatures.Auth.Commands.LoginForPartTime;
using Application.Features.GeneralManagementFeatures.Auth.Commands.LoginForWorker;
using Application.Features.GeneralManagementFeatures.Auth.Commands.LoginWithSystemAdmin;
using Application.Features.GeneralManagementFeatures.Auth.Commands.Register;
using Application.Features.GeneralManagementFeatures.Auth.Commands.UpdatePassword;
using Application.Features.GeneralManagementFeatures.Auth.Commands.UpdatePasswordBySystemAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.GeneralManagementControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;

        public AuthController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthCommand loginAuthCommand)
        {
            LoginAuthResponse response = await _mediator.Send(loginAuthCommand);
            return Ok(response);
        }

        [HttpPost("LoginForPartTime")]
        public async Task<IActionResult> LoginForPartTime([FromBody] LoginForPartTimeAuthCommand loginAuthCommand)
        {
            LoginForPartTimeAuthResponse response = await _mediator.Send(loginAuthCommand);
            return Ok(response);
        }

        [HttpPost("LoginForWorker")]
        public async Task<IActionResult> LoginForWorker([FromBody] LoginForWorkerAuthCommand loginAuthCommand)
        {
            LoginForWorkerAuthResponse response = await _mediator.Send(loginAuthCommand);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> LoginWithSystemAdmin([FromBody] LoginAuthWithSystemAdminCommand command)
        {
            LoginAuthWithSystemAdminResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAuthCommand registerAuthCommand)
        {
            RegisterAuthResponse response = await _mediator.Send(registerAuthCommand);
            return Ok(response);
        }

        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordAuthCommand updatePasswordAuthCommand)
        {
            UpdatePasswordAuthResponse response = await _mediator.Send(updatePasswordAuthCommand);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> UpdatePasswordBySystemAdmin([FromBody] UpdatePasswordAuthBySystemAdminCommand command)
        {
            UpdatePasswordAuthBySystemAdminResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTokenByRefreshToken([FromBody] GetTokenByRefreshTokenCommand request)
        {
            GetTokenByRefreshTokenResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteRefreshToken([FromBody] DeleteRefreshTokenCommand request)
        {
            DeleteRefreshTokenResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        //email test 

        [HttpPost("[action]")]
        public async Task<IActionResult> SendEmail()
        {
            await _mailService.SendMailAsync("tatlikasimislam@gmail.com", "konu test", "content test <br> elma attım denizer");
            return Ok();
        }

    }
}
