using Application.Abstractions.Token;
using Application.Features.Base;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.LoginForWorker
{
    public class LoginForWorkerAuthResponse : BaseResponse, IResponse
    {
        public Token? Token { get; set; }
        public Guid? SucceededLogGid { get; set; }
        public string? SessionId { get; set; }
        public bool IsPartTimeWorker { get; set; }
    }
}
