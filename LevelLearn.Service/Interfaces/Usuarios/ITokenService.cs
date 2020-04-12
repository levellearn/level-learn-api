using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Enums;
using LevelLearn.ViewModel.Usuarios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Usuarios
{
    public interface ITokenService
    {

        /// <summary>
        /// Gera o JWT para um usuário com determinada role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task<TokenVM> GerarJWT(ApplicationUser user, IList<string> roles);

        /// <summary>
        /// Gera um refresh token de determinado tamanho
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        string GerarRefreshToken(int size = 32);

        /// <summary>
        /// Obtêm o RefreshTokenData serializado
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns>Retorna o RefreshTokenData serializado</returns>
        Task<string> ObterRefreshTokenCache(string refreshToken);

        /// <summary>
        /// Invalida o token e refresh token do banco de cache
        /// </summary>
        /// <param name="jti">JWT ID</param>
        /// <returns></returns>
        Task InvalidarTokenERefreshTokenCache(string jti);

        /// <summary>
        /// Invalida o refresh token do banco de cache
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task InvalidarRefreshTokenCache(string refreshToken);

    }
}
