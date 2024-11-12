using Application.Abstractions.Token;
using Application.Repositories.GeneralManagementRepos.UserRepo;
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
                  new Claim("isSystemAdmin",user.IsSystemAdmin.ToString()),
                  new Claim("isLoginStatus",user.IsSystemAdmin.ToString())
            };


            T.Token token = new();

            //Security Key'in simetri�ini al�yoruz.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //�ifrelenmi� kimli�i olu�turuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Olu�turulacak token ayarlar�n� veriyoruz.
            token.AccessTokenExpiration = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.AccessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );

            //Token olu�turucu s�n�f�ndan bir �rnek alal�m.
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            token.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(minute + 20); //accesstokendan 20 dakika daha uzun s�rer
            return token;
        }
    }
}
