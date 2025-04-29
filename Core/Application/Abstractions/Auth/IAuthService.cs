using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using T = Application.Abstractions.Token;

namespace Application.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<LoginUserResponse> Login(LoginUserCommand loginAuthCommand);
        Task<T.Token> CreateTokenByRefreshToken(string refreshToken);
        Task<bool> RevokeRefreshToken(string refreshToken);
    }
}
