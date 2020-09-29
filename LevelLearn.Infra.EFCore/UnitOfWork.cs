using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Domain.Repositories.Pessoas;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Infra.EFCore.Contexts;
using LevelLearn.Infra.EFCore.Repositories.Institucional;
using LevelLearn.Infra.EFCore.Repositories.Pessoas;
using System.Threading.Tasks;

namespace LevelLearn.Infra.EFCore.UnityOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly LevelLearnContext _context;
        private IInstituicaoRepository _instituicoes;
        private ICursoRepository _cursos;
        private ITurmaRepository _turmas;
        private IPessoaRepository _pessoas;
        private IAlunoRepository _alunos;
        private IProfessorRepository _professores;

        public UnitOfWork(LevelLearnContext context)
        {
            _context = context;
        }

        public IInstituicaoRepository Instituicoes
        {
            get { return _instituicoes ??= new InstituicaoRepository(_context); }
        }

        public ICursoRepository Cursos
        {
            get { return _cursos ??= new CursoRepository(_context); }
        }

        public ITurmaRepository Turmas
        {
            get { return _turmas ??= new TurmaRepository(_context); }
        }

        public IPessoaRepository Pessoas
        {
            get { return _pessoas ??= new PessoaRepository(_context); }
        }

        public IAlunoRepository Alunos
        {
            get { return _alunos ??= new AlunoRepository(_context); }
        } 
        
        public IProfessorRepository Professores
        {
            get { return _professores ??= new ProfessorRepository(_context); }
        }

        public bool Commit() => _context.SaveChanges() > 0;

        public async Task<bool> CommitAsync() => await _context.SaveChangesAsync() > 0;      

        public void Dispose()
        {
            _context.Dispose();
            //if (_context != null) _context.Dispose();
            //GC.SuppressFinalize(this);
        }

    }
}
