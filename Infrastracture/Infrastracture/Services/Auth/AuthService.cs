using Application.Abstractions.Auth;
using Application.Abstractions.Token;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
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

        public AuthService(
            ITokenHandler tokenHandler,
            IUserReadRepository userReadRepository,
            IUserWriteRepository userWriteRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _tokenHandler = tokenHandler;
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _httpContextAccessor = httpContextAccessor;
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

            // Şifre kontrolü - projenizdeki şifre doğrulama mantığına göre değiştirin
            if (userToCheck.Password != loginUserCommand.Password) // Gerçek hayatta hash kontrolü yapılmalı!
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
            // Refresh token ile kullanıcıyı bul
            var user = await _userReadRepository.GetSingleAsync(u => 
                u.RefreshToken == refreshToken && 
                u.RefreshTokenExpiration > DateTime.UtcNow);

            if (user == null)
            {
                return null; // Geçersiz veya süresi dolmuş refresh token
            }

            // Yeni token oluştur
            var token = _tokenHandler.CreateAccessToken(user, 600);
            
            // Kullanıcının refresh token bilgilerini güncelle
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiration = token.RefreshTokenExpiration;
            
            _userWriteRepository.Update(user);
            await _userWriteRepository.SaveAsync();

            return token;
        }

        public async Task<bool> RevokeRefreshToken(string refreshToken)
        {
            // Refresh token ile kullanıcıyı bul
            var user = await _userReadRepository.GetSingleAsync(u => u.RefreshToken == refreshToken);
            
            if (user == null)
            {
                return false;
            }
            
            // Refresh token'ı sıfırla
            user.RefreshToken = null;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(-1); // Geçmiş bir tarih
            
            _userWriteRepository.Update(user);
            await _userWriteRepository.SaveAsync();
            
            return true;
        }

    }
}