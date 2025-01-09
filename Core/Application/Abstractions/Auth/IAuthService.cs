using Application.Features.GeneralManagementFeatures.Auth.Commands.Login;
using Application.Features.GeneralManagementFeatures.Auth.Commands.LoginForPartTime;
using Application.Features.GeneralManagementFeatures.Auth.Commands.LoginForWorker;
using Application.Features.GeneralManagementFeatures.Auth.Commands.LoginWithSystemAdmin;
using Application.Features.GeneralManagementFeatures.Auth.Commands.Register;
using Application.Features.GeneralManagementFeatures.Auth.Commands.UpdatePassword;
using Application.Features.GeneralManagementFeatures.Auth.Commands.UpdatePasswordBySystemAdmin;
using T = Application.Abstractions.Token;

namespace Application.Abstractions.Auth
{
    public interface IAuthService
    {
        public Task<RegisterAuthResponse> Register(RegisterAuthCommand registerAuthCommand);
        public Task<LoginAuthResponse> Login(LoginAuthCommand loginAuthCommand);
        public Task<LoginForPartTimeAuthResponse> LoginForPartTime(LoginForPartTimeAuthCommand loginAuthCommand);
        public Task<LoginForWorkerAuthResponse> LoginForWorker(LoginForWorkerAuthCommand loginAuthCommand);
        public Task<LoginAuthWithSystemAdminResponse> LoginWithSystemAdmin(LoginAuthWithSystemAdminCommand loginAuthWithSystemAdminCommand);
        public Task<UpdatePasswordAuthResponse> UpdatePassword(UpdatePasswordAuthCommand updatePasswordAuthCommand);
        public Task<UpdatePasswordAuthBySystemAdminResponse> UpdatePasswordBySystemAdmin(UpdatePasswordAuthBySystemAdminCommand updatePasswordAuthBySystemAdminCommand);
        public Task<T.Token> CreateTokenByRefreshToken(string refreshToken);
        public Task<bool> RevokeRefreshToken(string refreshToken);
        public Task ChangeForgottenPassword(string email);
    }
}
