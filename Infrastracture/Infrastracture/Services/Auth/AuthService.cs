using Application.Abstractions.Auth;
using Application.Abstractions.Token;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Application.Repositories.GeneralManagementRepo.AdminRepo;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
using Infrastracture.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using T = Application.Abstractions.Token;

namespace Infrastracture.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminReadRepository _adminReadRepository;
        private readonly IAdminWriteRepository _adminWriteRepository;
        public AuthService(
            ITokenHandler tokenHandler,
            IUserReadRepository userReadRepository,
            IUserWriteRepository userWriteRepository,
            IHttpContextAccessor httpContextAccessor,
            IAdminReadRepository adminReadRepository,
            IAdminWriteRepository adminWriteRepository)
        {
            _tokenHandler = tokenHandler;
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _adminReadRepository = adminReadRepository;
            _adminWriteRepository = adminWriteRepository;
        }

        public async Task<LoginUserResponse> Login(LoginUserCommand loginUserCommand)
        {
            var userToCheck = await _userReadRepository.GetSingleAsync(u => u.Email == loginUserCommand.Email);

            if (userToCheck == null)
            {
                return new LoginUserResponse()
                {
                    Title = "Hatalı İşlem",
                    Message = "Kullanıcı bulunamadı",
                    IsValid = false,
                    Token = null
                };
            }

            // Şifre kontrolü - hash ile doğrulama
            bool isPasswordValid = HashingHelperForAuth.VeriFyPasswordHash(
                loginUserCommand.Password,
                userToCheck.Password,
                userToCheck.PasswordSalt);

            if (!isPasswordValid)
            {
                return new LoginUserResponse()
                {
                    Title = "Hatalı İşlem",
                    Message = "E-posta veya şifre hatalı",
                    IsValid = false,
                    Token = null
                };
            }

            // Token oluştur
            T.Token token = _tokenHandler.CreateAccessToken(userToCheck, 600);
            var sessionId = Guid.NewGuid().ToString();

            // Refresh token'ı kullanıcı modelinde sakla
            userToCheck.RefreshToken = token.RefreshToken;
            userToCheck.RefreshTokenExpiration = token.RefreshTokenExpiration;
            
            _userWriteRepository.Update(userToCheck);
            await _userWriteRepository.SaveAsync();

            return new LoginUserResponse()
            {
                Message = "Giriş başarılı",
                IsValid = true,
                Token = token.AccessToken,
            };
        }

        public async Task<T.Token> CreateTokenByRefreshToken(string refreshToken)
        {
            // Önce User'larda arama yap
            var user = await _userReadRepository.GetSingleAsync(u =>
                u.RefreshToken == refreshToken &&
                u.RefreshTokenExpiration > DateTime.UtcNow);

            if (user != null)
            {
                // Kullanıcı bulundu, yeni token oluştur
                var token = _tokenHandler.CreateAccessToken(user, 600);

                // Kullanıcının refresh token bilgilerini güncelle
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpiration = token.RefreshTokenExpiration;

                _userWriteRepository.Update(user);
                await _userWriteRepository.SaveAsync();

                return token;
            }

            // User'larda bulunamadıysa Admin'lerde ara
            var admin = await _adminReadRepository.GetSingleAsync(a =>
                a.RefreshToken == refreshToken &&
                a.RefreshTokenExpiration > DateTime.UtcNow);

            if (admin != null)
            {
                // Admin bulundu, yeni token oluştur
                var token = _tokenHandler.CreateAccessToken(admin, 600);

                // Admin'in refresh token bilgilerini güncelle
                admin.RefreshToken = token.RefreshToken;
                admin.RefreshTokenExpiration = token.RefreshTokenExpiration;

                _adminWriteRepository.Update(admin);
                await _adminWriteRepository.SaveAsync();

                return token;
            }

            return null; // Geçerli refresh token bulunamadı
        }

        public async Task<bool> RevokeRefreshToken(string refreshToken)
        {
            bool revoked = false;

            // User'larda ara
            var user = await _userReadRepository.GetSingleAsync(u => u.RefreshToken == refreshToken);
            if (user != null)
            {
                // Refresh token'ı sıfırla
                user.RefreshToken = null;
                user.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(-1);

                _userWriteRepository.Update(user);
                await _userWriteRepository.SaveAsync();
                revoked = true;
            }

            // Admin'lerde ara
            var admin = await _adminReadRepository.GetSingleAsync(a => a.RefreshToken == refreshToken);
            if (admin != null)
            {
                // Refresh token'ı sıfırla
                admin.RefreshToken = null;
                admin.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(-1);

                _adminWriteRepository.Update(admin);
                await _adminWriteRepository.SaveAsync();
                revoked = true;
            }

            return revoked;
        }

    }
}