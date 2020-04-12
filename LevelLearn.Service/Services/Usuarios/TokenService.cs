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
using System.Security.Cryptography;
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
            var jti = Guid.NewGuid().ToString();
            var claims = CriarClaimsJWT(user, roles, jti);
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
            var refreshToken = GerarRefreshToken();

            await SalvarTokenCache(jti, refreshToken);
            await SalvarRefreshTokenCache(refreshToken, user.UserName);

            return new TokenVM()
            {
                Created = dataCriacao,
                Expiration = dataExpiracao,
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        private ICollection<Claim> CriarClaimsJWT(ApplicationUser user, IList<string> roles, string jti)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ApplicationClaims.PESSOA_ID, user.PessoaId.ToString()),
                new Claim(ClaimTypes.Name, user.NickName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public string GerarRefreshToken(int size = 32)
        {
            var randomNumber = new byte[size];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Salva o token como "chave" e o refreshToken como "valor"
        /// </summary>
        /// <param name="jti">JWT ID</param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        private async Task SalvarTokenCache(string jti, string refreshToken)
        {
            var expiracaoSegundos = _appSettings.JWTSettings.ExpiracaoSegundos;
            var tempoToleranciaSegundos = _appSettings.JWTSettings.TempoToleranciaSegundos;
            var expiracaoToken = TimeSpan.FromSeconds(expiracaoSegundos + tempoToleranciaSegundos);

            var opcoesCache = new DistributedCacheEntryOptions() { 
                AbsoluteExpirationRelativeToNow = expiracaoToken
            };

            await _redisCache.SetStringAsync(jti, refreshToken, opcoesCache);
        }

        /// <summary>
        /// Salva o Refresh Token como "chave" e o RefreshTokenData como "valor"
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private async Task SalvarRefreshTokenCache(string refreshToken, string userName)
        {
            var expiracaoRefreshToken = TimeSpan.FromSeconds(_appSettings.JWTSettings.RefreshTokenExpiracaoSegundos);

            var refreshTokenData = new RefreshTokenData(refreshToken, userName);
            string refreshTokenDataSerializado = JsonConvert.SerializeObject(refreshTokenData);

            var opcoesCache = new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(expiracaoRefreshToken);

            await _redisCache.SetStringAsync(refreshToken, refreshTokenDataSerializado, opcoesCache);
        }

        public async Task<string> ObterRefreshTokenCache(string refreshToken)
        {
            return await _redisCache.GetStringAsync(refreshToken);
        }

        public async Task InvalidarTokenERefreshTokenCache(string jti)
        {
            var refreshToken = await _redisCache.GetStringAsync(jti);
            await _redisCache.RemoveAsync(jti);
            await InvalidarRefreshTokenCache(refreshToken);
        }

        public async Task InvalidarRefreshTokenCache(string refreshToken)
        {
            await _redisCache.RemoveAsync(refreshToken);
        }

    }
}
