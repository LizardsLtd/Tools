using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Picums.Data.Domain;

namespace Picums.Data.CQRS.DataAccess
{
    public interface ICollectionDataReader<TSource>
        where TSource : IAggregateRoot
    {
        Task<IEnumerable<TSource>> All();

        Task<IEnumerable<TSource>> Where(Expression<Func<TSource, bool>> predicate);
    }
}