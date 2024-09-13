using Application.Abstractions.Auth;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.UpdatePassword
{
    public class UpdatePasswordAuthCommand : IRequest<UpdatePasswordAuthResponse>
    {
        public string Email { get; set; }
        public string OldPassword { get;set; }
        public string NewPassword { get; set; }

        public class UpdatePasswordAuthCommandHandler : IRequestHandler<UpdatePasswordAuthCommand, UpdatePasswordAuthResponse>
        {
            private readonly IAuthService _authService;

            public UpdatePasswordAuthCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<UpdatePasswordAuthResponse> Handle(UpdatePasswordAuthCommand request, CancellationToken cancellationToken)
            {
                UpdatePasswordAuthResponse response = await _authService.UpdatePassword(request);
                return response;
            }
        }

    }
}
