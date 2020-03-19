using LevelLearn.Domain.Entities;
using LevelLearn.Domain.Repositories;
using LevelLearn.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : EntityBase
    {
        protected readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _repository.CompleteAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _repository.CompleteAsync();
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

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetWithPagination(string query, int pageIndex, int pageSize)
        {
            return await _repository.GetWithPagination(query, pageIndex, pageSize);
        }

        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);
            _repository.Complete();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRange(entities);
            _repository.Complete();
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
            _repository.Complete();
        }

    }
}
