using Application.Abstractions.Auth;
using Application.Abstractions.Token;
using Application.Features.GeneralManagementFeatures.Auth.Constants;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.GetTokenByRefreshToken
{
    public class GetTokenByRefreshTokenCommand : IRequest<GetTokenByRefreshTokenResponse>
    {
        public string refreshToken { get; set; }

        public class LoginAuthCommandHandler : IRequestHandler<GetTokenByRefreshTokenCommand, GetTokenByRefreshTokenResponse>
        {
            private readonly IAuthService _authService;

            public LoginAuthCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<GetTokenByRefreshTokenResponse> Handle(GetTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                Token token = await _authService.CreateTokenByRefreshToken(request.refreshToken);

                if (token == null)
                {
                    return new GetTokenByRefreshTokenResponse()
                    {
                        IsValid = false,
                        Message = AuthBussinessMessages.TokenNotCreatedByRefresh,
                        ActionType = AuthBussinessMessages.Error,
                        Title = AuthBussinessMessages.ProcessError,
                    };
                }
                return new()
                {
                    Title = AuthBussinessMessages.ProcessCompleted,
                    Message = AuthBussinessMessages.SuccessCreatedTokenByRefreshMessage,
                    Token = token,
                    IsValid = true
                };
            }
        }
    }
}