using LevelLearn.Domain.Entities;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Repositories;
using LevelLearn.Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LevelLearn.Infra.EFCore.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly LevelLearnContext _context;

        public RepositoryBase(LevelLearnContext context)
        {
            _context = context;
        }

        #region Sync

        public TEntity Get(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll(int skip = 0, int limit = int.MaxValue)
        {
            return _context.Set<TEntity>()
                .Skip(skip)
                .Take(limit)
                .ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>()
                .Where(predicate)
                .ToList();
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        #endregion

        #region Async

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int skip = 0, int limit = int.MaxValue)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Skip(skip)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().CountAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetWithPagination(string query, int pageIndex, int pageSize)
        {
            pageIndex = (pageIndex <= 0) ? 1 : pageIndex;
            pageSize = (pageSize <= 0) ? 200 : pageSize;
            query = query.GenerateSlug();

            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Where(p => p.NomePesquisa.Contains(query))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(c => c.NomePesquisa)
                .ToListAsync();
        }
        
        public async Task<int> CountWithPagination(string query)
        {
            query = query.GenerateSlug();

            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Where(p => p.NomePesquisa.Contains(query))
                .CountAsync();
        }

        public async Task<bool> EntityExists(Expression<Func<TEntity, bool>> predicate)
        {
            bool entityExists = await _context.Set<TEntity>()
                        .AsNoTracking()
                        .AnyAsync(predicate);

            return entityExists;
        }

        #endregion

    }
}
