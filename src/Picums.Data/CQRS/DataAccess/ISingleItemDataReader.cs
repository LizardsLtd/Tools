using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Picums.Data.Domain;
using Picums.Maybe;

namespace Picums.Data.CQRS.DataAccess
{
    public interface ISingleItemDataReader<TSource>
        where TSource : IAggregateRoot
    {
        Task<Maybe<TSource>> SingleOrDefault(Expression<Func<TSource, bool>> predicate);

        Task<Maybe<TSource>> FirstOrDefault(Expression<Func<TSource, bool>> predicate);

        Task<Maybe<TSource>> ById(Guid id);
    }
}