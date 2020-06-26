using LevelLearn.Domain.Entities;
using LevelLearn.Domain.Repositories;
using LevelLearn.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services
{
    public abstract class ServiceBase<TEntity, TKey> : IServiceBase<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly IRepositoryBase<TEntity, TKey> _repository;

        public ServiceBase(IRepositoryBase<TEntity, TKey> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _repository.CommitAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _repository.CommitAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task<int> CountWithPagination(string query)
        {
            return await _repository.CountWithPagination(query);
        }

        public async Task<bool> EntityExists(Expression<Func<TEntity, bool>> filter)
        {
            return await _repository.EntityExists(filter);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _repository.FindAsync(filter);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int skip = 0, int limit = int.MaxValue)
        {
            return await _repository.GetAllAsync(skip, limit);
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetWithPagination(string query, int pageNumber, int pageSize)
        {
            return await _repository.GetWithPagination(query, pageNumber, pageSize);
        }

        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);
            _repository.Commit();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRange(entities);
            _repository.Commit();
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
            _repository.Commit();
        }

    }
}
