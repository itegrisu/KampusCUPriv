using Application.Abstractions.Auth;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.Register
{
    public class RegisterAuthCommand : IRequest<RegisterAuthResponse>
    {

        public string Adi { get; set; } = string.Empty;
        public string Soyadi { get; set; } = string.Empty;
        public string EPosta { get; set; } = string.Empty;
        public string Gsm { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;


        public class RegisterAuthCommandHandler : IRequestHandler<RegisterAuthCommand, RegisterAuthResponse>
        {
            private readonly IAuthService _authService;

            public RegisterAuthCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<RegisterAuthResponse> Handle(RegisterAuthCommand request, CancellationToken cancellationToken)
            {
                RegisterAuthResponse response = await _authService.Register(request);

                return response;
            }
        }

    }
}
