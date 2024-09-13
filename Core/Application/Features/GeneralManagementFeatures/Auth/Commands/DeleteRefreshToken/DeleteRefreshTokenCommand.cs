using Application.Abstractions.Auth;
using Application.Features.GeneralManagementFeatures.Auth.Constants;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.DeleteRefreshToken
{
    public class DeleteRefreshTokenCommand : IRequest<DeleteRefreshTokenResponse>
    {
        public string refreshToken { get; set; }

        public class DeleteRefreshTokenCommandHandler : IRequestHandler<DeleteRefreshTokenCommand, DeleteRefreshTokenResponse>
        {
            private readonly IAuthService _authService;

            public DeleteRefreshTokenCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<DeleteRefreshTokenResponse> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var result = await _authService.RevokeRefreshToken(request.refreshToken);
                if (result == true)
                {
                    return new()
                    {
                        Title = AuthBussinessMessages.ProcessCompleted,
                        Message = AuthBussinessMessages.SuccessDeletedRefreshTokenMessage,
                        IsValid = true,


                    };
                }
                return new()
                {
                    ActionType = AuthBussinessMessages.Error,
                    Message = AuthBussinessMessages.RefreshTokenNotFound,
                    IsValid = false,
                    Title = AuthBussinessMessages.ProcessError
                };
            }
        }
    }
}
