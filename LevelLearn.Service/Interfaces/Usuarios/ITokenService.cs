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
        Task<TokenVM> GerarJWT(ApplicationUser user, IList<string> roles);
    }
}
