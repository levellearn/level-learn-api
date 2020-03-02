using LevelLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        TEntity Get(Guid id);
        Task<TEntity> GetAsync(Guid id);

        IEnumerable<TEntity> GetAll(int skip, int limit);
        Task<IEnumerable<TEntity>> GetAllAsync(int skip = 0, int limit = int.MaxValue);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        Task AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        int Count();
        Task<int> CountAsync();
    }
}
