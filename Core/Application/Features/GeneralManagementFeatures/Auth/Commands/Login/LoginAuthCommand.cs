using Application.Abstractions.Auth;
using Application.Features.GeneralManagementFeatures.Auth.Constants;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.Login
{
    public class LoginAuthCommand : IRequest<LoginAuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginAuthCommandHandler : IRequestHandler<LoginAuthCommand, LoginAuthResponse>
        {
            private readonly IAuthService _authService;
            private readonly IUserReadRepository _userReadRepository;
            public LoginAuthCommandHandler(IAuthService authService, IUserReadRepository userReadRepository)
            {
                _authService = authService;
                _userReadRepository = userReadRepository;
            }

            public async Task<LoginAuthResponse> Handle(LoginAuthCommand request, CancellationToken cancellationToken)
            {
                var user = await _userReadRepository.GetSingleAsync(x => x.EPosta == request.Email);
                if (user != null && user.AktifHesapMi == true)
                {
                    LoginAuthResponse response = await _authService.Login(request);
                    return response;
                }

                return new LoginAuthResponse
                {
                    Title = AuthBussinessMessages.LoginFailed,
                    Message = AuthBussinessMessages.NotAuthorisedToLogIn
                };
            }
        }

    }
}
