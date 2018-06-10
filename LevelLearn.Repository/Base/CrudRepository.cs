using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LevelLearn.Repository.Base
{
    public abstract class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        public CrudRepository(DbContext context)
        {
            _context = context;
        }

        public virtual List<TEntity> Select(Func<TEntity, bool> where = null)
        {
            IEnumerable<TEntity> resultado = _context.Set<TEntity>().AsNoTracking();
            if (where != null)
                resultado = resultado.Where(where);
            return resultado.ToList();
        }

        public virtual List<TEntity> SelectIncludes(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            IEnumerable<TEntity> resultado = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));


            if (where != null)
                resultado = resultado.Where(where);

            return resultado.ToList();
        }

        public virtual TEntity SelectById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual bool Insert(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public virtual bool Update(TEntity entity)
        {
            try
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                    _context.Set<TEntity>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual bool Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Attach(entity);
                _context.Entry(entity).State = EntityState.Deleted;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual bool DeleteById(int id)
        {
            TEntity entity = SelectById(id);
            return Delete(entity);
        }

        public void Detach(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
            _context.SaveChanges();
        }

        public void DetachAll()
        {
            foreach (EntityEntry dbEntityEntry in _context.ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity != null)
                {
                    dbEntityEntry.State = EntityState.Detached;
                }
            }
        }
    }
}
