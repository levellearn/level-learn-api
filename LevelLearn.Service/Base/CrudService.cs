using LevelLearn.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LevelLearn.Service.Base
{
    public abstract class CrudService<TEntity> : ICrudService<TEntity> where TEntity : class
    {
        protected readonly ICrudRepository<TEntity> _repository;
        public CrudService(ICrudRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual bool Delete(TEntity entity)
        {
            return _repository.Delete(entity);
        }

        public virtual bool DeleteById(int id)
        {
            return _repository.DeleteById(id);
        }

        public virtual bool Insert(TEntity entity)
        {
            return _repository.Insert(entity);
        }

        public List<TEntity> Select(Func<TEntity, bool> where = null)
        {
            return _repository.Select(where);
        }

        public TEntity SelectById(int id)
        {
            return _repository.SelectById(id);
        }

        public List<TEntity> SelectIncludes(Func<TEntity, bool> where = null, params Expression<Func<TEntity, object>>[] includes)
        {
            return _repository.SelectIncludes(where, includes);
        }

        public virtual bool Update(TEntity entity)
        {
            return _repository.Update(entity);
        }
    }
}
