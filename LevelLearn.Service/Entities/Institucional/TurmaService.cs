using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Interfaces.Institucional;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Institucional;

namespace LevelLearn.Service.Entities.Institucional
{
    public class TurmaService : CrudService<Turma>, ITurmaService
    {
        public TurmaService(ITurmaRepository turmaRepository)
            : base(turmaRepository)
        { }
    }
}
