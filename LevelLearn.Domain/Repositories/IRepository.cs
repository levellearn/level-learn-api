using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        Task<TEntity> GetAsync(Guid id);

        IEnumerable<TEntity> GetAll(int skip, int limit);
        Task<IEnumerable<TEntity>> GetAllAsync(int skip = 0, int limit = int.MaxValue);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        int Count();
        Task<int> CountAsync();
    }
}
