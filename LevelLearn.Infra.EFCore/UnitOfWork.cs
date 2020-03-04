using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Infra.EFCore.Contexts;
using LevelLearn.Infra.EFCore.Repositories.Institucional;
using System.Threading.Tasks;

namespace LevelLearn.Infra.EFCore.UnityOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly LevelLearnContext _context;

        public UnitOfWork(LevelLearnContext context)
        {
            _context = context;
            //Instituicoes = new InstituicaoRepository(_context);
        }

        public IInstituicaoRepository Instituicoes => new InstituicaoRepository(_context);

        //public IInstituicaoRepository Instituicoes { get; private set; }


        public bool Complete()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public async Task<bool> CompleteAsync()
        {
            var numberEntriesSaved = await _context.SaveChangesAsync();
            return numberEntriesSaved > 0 ? true : false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
