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

        IEnumerable<TEntity> GetAll(int skip = 0, int limit = int.MaxValue);
        Task<IEnumerable<TEntity>> GetAllAsync(int skip = 0, int limit = int.MaxValue);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter);

        void Add(TEntity entity);
        Task AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        int Count();
        Task<int> CountAsync();

        Task<IEnumerable<TEntity>> GetWithPagination(string query, int pageIndex, int pageSize);
        Task<int> CountWithPagination(string query);
        Task<bool> EntityExists(Expression<Func<TEntity, bool>> filter);

        //List<TEntity> SelectIncludes(Func<TEntity, bool> where = null, params Expression<Func<TEntity, object>>[] includes);
        bool Complete();
        Task<bool> CompleteAsync();
    }
}
