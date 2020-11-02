using Cinematheque.Data.DAO;
using Cinematheque.Data.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Cinematheque.Data.Dao.Impl
{
    public abstract class AbstractDao<TEntity> : IDao<TEntity> where TEntity : class
    {
        public virtual void Add(TEntity entity)
        {
            using(var context = new CinemathequeContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using(var context = new CinemathequeContext())
            {
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }

        public TEntity Find(object id)
        {
            using(var context = new CinemathequeContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> includePath = null)
        {
            using(var context = new CinemathequeContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (includePath != null)
                {
                    query = query.Include(includePath);
                }

                return query.ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using(var context = new CinemathequeContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
