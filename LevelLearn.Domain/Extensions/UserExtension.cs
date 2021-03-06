﻿using LevelLearn.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace LevelLearn.Domain.Extensions
{
    public static class UserExtension
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            string userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }

        public static Guid GetPessoaId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            string pessoaId = principal.FindFirst(UserClaims.PESSOA_ID)?.Value;
            return new Guid(pessoaId);
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static IEnumerable<string> GetUserRoles(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindAll(ClaimTypes.Role).Select(r => r.Value);
        }

        public static string GetJWTokenId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
        }

    }
}
