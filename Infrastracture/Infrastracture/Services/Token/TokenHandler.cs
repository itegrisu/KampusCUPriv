using Application.Abstractions.Token;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Domain.Entities.GeneralManagements;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using T = Application.Abstractions.Token;

namespace Infrastracture.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;
        private readonly IUserReadRepository _userReadRepository;

        public TokenHandler(IConfiguration configuration, IUserReadRepository userReadRepository)
        {
            _configuration = configuration;
            _userReadRepository = userReadRepository;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        public T.Token CreateAccessToken(User user, int minute = 600)
        {
            var claims = new List<Claim>
            {
                  new Claim(ClaimTypes.NameIdentifier, user.Gid.ToString()),
                  //new Claim("http://schemas.yourdomain.com/claims/email",user.Email),
                  //new Claim("http://schemas.yourdomain.com/claims/isSystemAdmin", user.IsSystemAdmin.ToString()),
                  //new Claim("http://schemas.yourdomain.com/claims/isLoginStatus", user.IsLoginStatus.ToString())

                  new Claim(ClaimTypes.Email,user.Email),
            };


            T.Token token = new();

            //Security Key'in simetriðini alýyoruz.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Þifrelenmiþ kimliði oluþturuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluþturulacak token ayarlarýný veriyoruz.
            token.AccessTokenExpiration = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.AccessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );

            //Token oluþturucu sýnýfýndan bir örnek alalým.
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            token.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(minute + 20); //accesstokendan 20 dakika daha uzun sürer
            return token;
        }

        public T.Token CreateAccessToken(Admin admin, int minute = 600)
        {
            var claims = new List<Claim>
            {
                  new Claim(ClaimTypes.NameIdentifier, admin.Gid.ToString()),
                  //new Claim("http://schemas.yourdomain.com/claims/email",user.Email),
                  //new Claim("http://schemas.yourdomain.com/claims/isSystemAdmin", user.IsSystemAdmin.ToString()),
                  //new Claim("http://schemas.yourdomain.com/claims/isLoginStatus", user.IsLoginStatus.ToString())

                  new Claim(ClaimTypes.Email,admin.Email),
            };


            T.Token token = new();

            //Security Key'in simetriðini alýyoruz.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Þifrelenmiþ kimliði oluþturuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluþturulacak token ayarlarýný veriyoruz.
            token.AccessTokenExpiration = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.AccessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );

            //Token oluþturucu sýnýfýndan bir örnek alalým.
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            token.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(minute + 20); //accesstokendan 20 dakika daha uzun sürer
            return token;
        }

    }
}
