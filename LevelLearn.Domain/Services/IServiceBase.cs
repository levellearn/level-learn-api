﻿using LevelLearn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Services
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync(int skip = 0, int limit = int.MaxValue);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        Task<int> CountAsync();

        Task<IEnumerable<TEntity>> GetWithPagination(string query, int pageIndex, int pageSize);
        Task<int> CountWithPagination(string query);
        Task<bool> EntityExists(Expression<Func<TEntity, bool>> filter);
    }
}