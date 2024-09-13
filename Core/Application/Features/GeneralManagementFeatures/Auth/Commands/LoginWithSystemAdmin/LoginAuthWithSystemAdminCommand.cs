using Application.Abstractions.Auth;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.LoginWithSystemAdmin
{
    public class LoginAuthWithSystemAdminCommand : IRequest<LoginAuthWithSystemAdminResponse>
    {
        public Guid Gid { get; set; } //this is the system admin gid
        public Guid GidUser { get; set; } //this is the user Gid

        public class LoginAuthWithSystemAdminCommandHandler : IRequestHandler<LoginAuthWithSystemAdminCommand, LoginAuthWithSystemAdminResponse>
        {
            private readonly IAuthService _authService;

            public LoginAuthWithSystemAdminCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<LoginAuthWithSystemAdminResponse> Handle(LoginAuthWithSystemAdminCommand request, CancellationToken cancellationToken)
            {
                LoginAuthWithSystemAdminResponse response = await _authService.LoginWithSystemAdmin(request);
                return response;
            }
        }

    }
}
