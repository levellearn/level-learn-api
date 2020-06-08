using LevelLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
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

        /// <summary>
        /// Retorna uma lista paginada com filtro
        /// </summary>
        /// <param name="searchFilter">Termo de pesquisa</param>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetWithPagination(string searchFilter, int pageNumber, int pageSize);
        /// <summary>
        /// Retorna o total para a paginação
        /// </summary>
        /// <param name="searchFilter">Termo de pesquisa</param>
        /// <returns></returns>
        Task<int> CountWithPagination(string searchFilter);

        Task<bool> EntityExists(Expression<Func<TEntity, bool>> filter);

        //List<TEntity> SelectIncludes(Func<TEntity, bool> where = null, params Expression<Func<TEntity, object>>[] includes);

        bool Complete();
        Task<bool> CompleteAsync();
    }
}
