using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Picums.Data.Domain;
using Picums.Maybe;

namespace Picums.Data.CQRS.DataAccess
{
    public interface IDataReader<TSource>
        where TSource : IAggregateRoot
    {
        Task<IEnumerable<TSource>> Collection(Func<TSource, bool> predicate);

        Task<Maybe<TSource>> Single(Func<TSource, bool> predicate, Func<IEnumerable<TSource>, TSource> reduce);
    }
}