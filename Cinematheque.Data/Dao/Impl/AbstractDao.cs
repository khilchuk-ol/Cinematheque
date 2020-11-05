using Cinematheque.Data.DAO;
using Cinematheque.Data.Data;
using Cinematheque.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Cinematheque.Data.Dao.Impl
{
    public abstract class AbstractDao<TEntity> : IDao<TEntity> where TEntity : class
    {
        protected CinemathequeContext Context { get; }

        public AbstractDao(CinemathequeContext context)
        {
            Validator.RequireNotNull(context);

            Context = context;
            Context.Database.Log = log => LogWriter.Log("DATABASE LOG: " + log);
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();           
        }

        public void Delete(TEntity entity)
        {          
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();           
        }

        public TEntity Find(object id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> includePath = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (includePath != null)
            {
                query = query.Include(includePath);
            }

            return query.ToList();          
        }

        public void Update(TEntity entity)
        {            
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();           
        }
    }
}
