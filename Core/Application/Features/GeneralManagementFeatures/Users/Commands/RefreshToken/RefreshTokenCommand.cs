using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public string RefreshToken { get; set; }

        public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
        {
            private readonly Application.Abstractions.Auth.IAuthService _authService;

            public RefreshTokenCommandHandler(Application.Abstractions.Auth.IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var token = await _authService.CreateTokenByRefreshToken(request.RefreshToken);

                if (token == null)
                {
                    return new RefreshTokenResponse
                    {
                        Title = "İşlem Başarısız",
                        Message = "Geçersiz veya süresi dolmuş refresh token",
                        IsValid = false,
                        AccessToken = null
                    };
                }

                return new RefreshTokenResponse
                {
                    Title = "İşlem Başarılı",
                    Message = "Token başarıyla yenilendi",
                    IsValid = true,
                    AccessToken = token.AccessToken,
                    RefreshToken = token.RefreshToken,
                    Expiration = token.AccessTokenExpiration
                };
            }
        }
    }
}
