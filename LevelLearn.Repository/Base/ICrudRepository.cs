using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LevelLearn.Repository.Base
{
    public interface ICrudRepository<TEntity> where TEntity : class
    {
        List<TEntity> Select(Func<TEntity, bool> where = null);
        List<TEntity> SelectIncludes(Func<TEntity, bool> where = null, params Expression<Func<TEntity, object>>[] includes);
        TEntity SelectById(int id);

        bool Insert(TEntity entity);
        bool Update(TEntity entity);

        bool Delete(TEntity entity);
        bool DeleteById(int id);

        void Detach(TEntity entity);
        void DetachAll();
    }
}
