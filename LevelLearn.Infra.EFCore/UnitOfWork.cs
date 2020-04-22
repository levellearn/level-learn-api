using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Domain.Repositories.Pessoas;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Infra.EFCore.Contexts;
using System.Threading.Tasks;

namespace LevelLearn.Infra.EFCore.UnityOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly LevelLearnContext _context;

        public UnitOfWork(
            LevelLearnContext context,
            IInstituicaoRepository instituicaoRepository,
            IPessoaRepository pessoaRepository
            )
        {
            _context = context;
            Instituicoes = instituicaoRepository;
            Pessoas = pessoaRepository;
        }

        //public IInstituicaoRepository Instituicoes => new InstituicaoRepository(_context);
        //public IPessoaRepository Pessoas => new PessoaRepository(_context);

        public IInstituicaoRepository Instituicoes { get; }
        public IPessoaRepository Pessoas { get; }


        public bool Complete()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
