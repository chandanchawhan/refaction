using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace refactor_me.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Remove(TEntity entity);
    }
}
