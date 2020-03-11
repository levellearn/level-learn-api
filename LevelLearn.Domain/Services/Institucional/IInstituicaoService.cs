using LevelLearn.Domain.Entities.Institucional;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Services.Institucional
{
    public interface IInstituicaoService : IDisposable
    {
        Task<ResponseAPI> CadastrarInstituicao(Instituicao instituicao);
    }
}
