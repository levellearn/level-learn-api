using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace LevelLearn.Domain.Extensions
{
    public static class UserExtension
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;

            return httpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

    }
}
