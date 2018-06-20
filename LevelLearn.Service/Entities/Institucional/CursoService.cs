using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Interfaces.Institucional;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Institucional;

namespace LevelLearn.Service.Entities.Institucional
{
    public class CursoService : CrudService<Curso>, ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        public CursoService(ICursoRepository cursoRepository)
            : base(cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }
    }
}
