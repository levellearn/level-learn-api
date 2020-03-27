using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
        private readonly AppSettings _appSettings;
        private readonly IDistributedCache _redisCache;

        public TokenService(IOptions<AppSettings> appSettings, IDistributedCache redisCache)
        {
            _appSettings = appSettings.Value;
            _redisCache = redisCache;
        }

        public async Task<TokenVM> GerarJWT(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ApplicationClaims.PESSOA_ID, user.PessoaId.ToString()),
                new Claim(ClaimTypes.Name, user.NickName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = Encoding.ASCII.GetBytes(_appSettings.JWTSettings.ChavePrivada);
            var dataCriacao = DateTime.UtcNow;
            var dataExpiracao = DateTime.UtcNow.AddSeconds(_appSettings.JWTSettings.ExpiracaoSegundos);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = dataExpiracao,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _appSettings.JWTSettings.Emissor,
                Audience = _appSettings.JWTSettings.ValidoEm,
                NotBefore = dataCriacao
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            var refreshToken = Guid.NewGuid().ToString();

            await SalvarRefreshTokenCache(user.UserName, refreshToken);

            return new TokenVM()
            {
                Created = dataCriacao,
                Expiration = dataExpiracao,
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        private async Task SalvarRefreshTokenCache(string userName, string refreshToken)
        {
            var expiracaoRefreshToken = TimeSpan.FromSeconds(_appSettings.JWTSettings.RefreshTokenExpiracaoSegundos);

            var refreshTokenData = new RefreshTokenData(refreshToken, userName);

            var opcoesCache = new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(expiracaoRefreshToken);
            await _redisCache.SetStringAsync(refreshToken, JsonConvert.SerializeObject(refreshTokenData), opcoesCache);
        }
    }
}
