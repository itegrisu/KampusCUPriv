using Application.Abstractions.Token;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.LoginForPartTime
{
    public class LoginForPartTimeAuthResponse : BaseResponse, IResponse
    {
        public Token? Token { get; set; }
        public Guid? SucceededLogGid { get; set; }
        public string? SessionId { get; set; }
        public bool IsPartTimeWorker { get; set; }
    }
}
