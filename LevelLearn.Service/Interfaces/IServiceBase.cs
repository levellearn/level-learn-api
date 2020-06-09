using LevelLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces
{
    public interface IServiceBase<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TEntity> GetAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAllAsync(int skip = 0, int limit = int.MaxValue);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        Task<int> CountAsync();

        Task<IEnumerable<TEntity>> GetWithPagination(string query, int pageNumber, int pageSize);
        Task<int> CountWithPagination(string query);
        Task<bool> EntityExists(Expression<Func<TEntity, bool>> filter);
    }
}
