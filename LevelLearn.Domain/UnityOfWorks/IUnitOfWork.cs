using System;
using System.Threading.Tasks;

namespace LevelLearn.Domain.UnityOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        //ICustomerRepository Customers { get; }

        bool Complete();
        Task<bool> CompleteAsync();
    }
}
