using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Interfaces.Pessoas;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Pessoas;

namespace LevelLearn.Service.Entities.Pessoas
{
    public class PessoaCursoService : CrudService<PessoaCurso>, IPessoaCursoService
    {
        private readonly IPessoaCursoRepository _pessoaCursoRepository;
        public PessoaCursoService(IPessoaCursoRepository pessoaCursoRepository)
            : base(pessoaCursoRepository)
        {
            _pessoaCursoRepository = pessoaCursoRepository;
        }
    }
}
