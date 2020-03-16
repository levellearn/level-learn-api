using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.ViewModel.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LevelLearn.Service.Services.Auth
{
    public interface ITokenService
    {
        Token GenerateToken();
    }

    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Token GenerateToken()
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.ChavePrivada);
            var dataExpiracao = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "teste"),
                    //new Claim(ClaimTypes.Name, user.Username.ToString()),
                    //new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = dataExpiracao,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new Token()
            {
                Authenticated = true,
                Created = DateTime.UtcNow,
                Expiration = dataExpiracao,
                AccessToken = token
            };
        }


    }
}
