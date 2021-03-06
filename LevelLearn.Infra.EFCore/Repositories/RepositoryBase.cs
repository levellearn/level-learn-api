﻿using LevelLearn.Domain.Entities;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LevelLearn.Infra.EFCore.Repository
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        #region Sync

        public TEntity Get(TKey id, bool asNoTracking)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

            if (asNoTracking) query = query.AsNoTracking();

            return query.SingleOrDefault(e => e.Id.Equals(id));
        }

        public IEnumerable<TEntity> GetAll(int skip = 0, int limit = int.MaxValue)
        {
            return _context.Set<TEntity>()
                .Skip(skip)
                .Take(limit)
                .ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>()
                .Where(filter)
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

        public virtual void Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Set<TEntity>().Attach(entity);

            _context.Set<TEntity>().Update(entity);
        }

        public void Remove(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Set<TEntity>().Attach(entity);

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

        public async Task<TEntity> GetAsync(TKey id, bool asNoTracking)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

            if (asNoTracking) query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int skip = 0, int limit = int.MaxValue)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Skip(skip)
                .Take(limit)
                .OrderBy(c => c.NomePesquisa)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Where(filter)
                .OrderBy(c => c.NomePesquisa)
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

        public virtual async Task<IEnumerable<TEntity>> GetWithPagination(string searchFilter, int pageNumber, int pageSize)
        {
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            pageSize = (pageSize <= 0) ? 1 : pageSize;
            searchFilter = searchFilter.GenerateSlug();

            return await _context.Set<TEntity>()
                //.IgnoreQueryFilters()
                .AsNoTracking()
                .Where(p => p.NomePesquisa.Contains(searchFilter))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(c => c.NomePesquisa)
                .ToListAsync();
        }

        public async Task<int> CountWithPagination(string searchFilter)
        {
            searchFilter = searchFilter.GenerateSlug();

            return await _context.Set<TEntity>()
                .AsNoTracking()
                .Where(p => p.NomePesquisa.Contains(searchFilter))
                .CountAsync();
        }

        public async Task<bool> EntityExists(Expression<Func<TEntity, bool>> filter)
        {
            bool entityExists = await _context.Set<TEntity>()
                        .AsNoTracking()
                        .AnyAsync(filter);

            return entityExists;
        }

        #endregion

        public bool Commit()
        {
            var numberEntriesSaved = _context.SaveChanges();
            return numberEntriesSaved > 0;
        }

        public async Task<bool> CommitAsync()
        {
            var numberEntriesSaved = await _context.SaveChangesAsync();
            return numberEntriesSaved > 0;
        }


    }
}
