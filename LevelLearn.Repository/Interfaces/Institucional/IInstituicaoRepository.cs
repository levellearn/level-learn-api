using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Base;

namespace LevelLearn.Repository.Interfaces.Institucional
{
    public interface IInstituicaoRepository : ICrudRepository<Instituicao>
    {
        bool IsAdmin(int instituicaoId, int pessoaId);
    }
}
