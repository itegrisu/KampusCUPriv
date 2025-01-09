using Application.Abstractions.Auth;
using Application.Features.GeneralManagementFeatures.Auth.Constants;
using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.LoginForPartTime
{
    public class LoginForPartTimeAuthCommand : IRequest<LoginForPartTimeAuthResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class LoginForPartTimeAuthCommandHandler : IRequestHandler<LoginForPartTimeAuthCommand, LoginForPartTimeAuthResponse>
        {
            private readonly IAuthService _authService;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;

            public LoginForPartTimeAuthCommandHandler(IAuthService authService, IUserReadRepository userReadRepository, IPartTimeWorkerReadRepository partTimeWorkerReadRepository)
            {
                _authService = authService;
                _userReadRepository = userReadRepository;
                _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
            }

            public async Task<LoginForPartTimeAuthResponse> Handle(LoginForPartTimeAuthCommand request, CancellationToken cancellationToken)
            {
                var user = await _partTimeWorkerReadRepository.GetSingleAsync(x => x.UserName == request.Username);
                if (user != null && user.IsLoginStatus == true)
                {
                    LoginForPartTimeAuthResponse response = await _authService.LoginForPartTime(request);
                    response.IsPartTimeWorker = true;
                    return response;
                }

                return new LoginForPartTimeAuthResponse
                {
                    Title = AuthBussinessMessages.LoginFailed,
                    Message = AuthBussinessMessages.NotAuthorisedToLogIn
                };
            }
        }
    }
}
