using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Entities.Pessoas;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Pessoas;

namespace LevelLearn.Service.Entities.Pessoas
{
    public class AlunoTurmaService : CrudService<AlunoTurma>, IAlunoTurmaService
    {
        public AlunoTurmaService(AlunoTurmaRepository alunoTurmaRepository)
            : base(alunoTurmaRepository)
        { }
    }
}
