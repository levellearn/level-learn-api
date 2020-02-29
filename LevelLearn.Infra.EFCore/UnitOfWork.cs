using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Infra.EFCore.Contexts;
using System.Threading.Tasks;

namespace J1DesignDigital.Infra.EFCore.UnityOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly LevelLearnContext _context;

        public UnitOfWork(LevelLearnContext context)
        {
            _context = context;
            //Customers = new CustomerRepository(_context);
        }


        //public ICustomerRepository Customers { get; private set; }


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
