using Application.Abstractions.Token;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.GetTokenByRefreshToken
{
    public class GetTokenByRefreshTokenResponse : BaseResponse, IResponse
    {
        public Token Token { get; set; }
    }
}
