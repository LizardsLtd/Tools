using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Picums.Data.Domain;
using Picums.Maybe;

namespace Picums.Data.CQRS.DataAccess
{
    public interface IDataReader<TSource>
        where TSource : IAggregateRoot
    {
        Task<IEnumerable<TSource>> All();

        Task<TResult> QueryFor<TResult>(Expression<Func<IQueryable<TSource>, TResult>> predicate);

        Task<IQueryable<TSource>> Where(Expression<Func<TSource, bool>> predicate);

        Task<Maybe<TSource>> SingleOrDefault(Expression<Func<TSource, bool>> predicate);
    }
}