using LevelLearn.Domain.Repositories;
using LevelLearn.Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace J1DesignDigital.Infra.EFCore.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly LevelLearnContext _context;

        public Repository(LevelLearnContext context)
        {
            _context = context;
        }

        #region Sync

        public TEntity Get(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity Get(long id)
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

        public async Task<TEntity> GetAsync(long id)
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

        #endregion

    }
}
