﻿using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.ViewModel.Auth;
using LevelLearn.ViewModel.Usuarios;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LevelLearn.Service.Services.Usuarios
{
    public interface ITokenService
    {
        Token GerarJWT(Pessoa pessoa);
    }

    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Token GerarJWT(Pessoa pessoa)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.ChavePrivada);
            var dataCriacao = DateTime.UtcNow;
            var dataExpiracao = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, pessoa.Id.ToString()),
                    new Claim(ClaimTypes.Name, pessoa.UserName)
                    //new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = dataExpiracao,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
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
