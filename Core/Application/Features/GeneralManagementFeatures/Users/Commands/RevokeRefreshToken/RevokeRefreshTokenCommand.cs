using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommand : IRequest<RevokeRefreshTokenResponse>
    {
        public string RefreshToken { get; set; }

        public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, RevokeRefreshTokenResponse>
        {
            private readonly Application.Abstractions.Auth.IAuthService _authService;

            public RevokeRefreshTokenCommandHandler(Application.Abstractions.Auth.IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<RevokeRefreshTokenResponse> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var result = await _authService.RevokeRefreshToken(request.RefreshToken);

                if (!result)
                {
                    return new RevokeRefreshTokenResponse
                    {
                        Title = "İşlem Başarısız",
                        Message = "Geçersiz token veya token bulunamadı",
                        IsValid = false
                    };
                }

                return new RevokeRefreshTokenResponse
                {
                    Title = "İşlem Başarılı",
                    Message = "Token başarıyla iptal edildi",
                    IsValid = true
                };
            }
        }
    }
}
