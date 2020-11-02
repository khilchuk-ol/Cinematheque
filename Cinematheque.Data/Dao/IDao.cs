using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cinematheque.Data.DAO
{
    public interface IDao<TEntity> where TEntity : class
    {
		TEntity Find(object id);

		IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> includePath = null);

		void Add(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);
	}
}
