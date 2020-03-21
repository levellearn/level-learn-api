using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Usuarios
{
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IOptions<JWTSettings> jwtSettings, UserManager<ApplicationUser> userManager)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
        }

        public async Task<Token> GerarJWT(ApplicationUser user, IList<string> roles)
        {            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.NickName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = Encoding.ASCII.GetBytes(_jwtSettings.ChavePrivada);
            var dataCriacao = DateTime.UtcNow;
            var dataExpiracao = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiracaoMinutos);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = dataExpiracao,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.ValidoEm,
                NotBefore = dataCriacao,
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new Token()
            {
                Authenticated = true,
                Created = dataCriacao,
                Expiration = dataExpiracao,
                AccessToken = token
            };
        }
        
    }
}
