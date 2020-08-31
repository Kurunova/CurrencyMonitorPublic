using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CurrencyMonitor.Core.Contracts.DataAccess
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(IEnumerable<TEntity> entity);

        void Add(TEntity entity);

        void Update(TEntity entity);
    }
}