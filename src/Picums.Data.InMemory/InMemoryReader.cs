using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;
using Picums.Maybe;

namespace Picums.Data.InMemory
{
    public sealed class InMemoryReader<T> : IDataReader<T>
        where T : IAggregateRoot
    {
        private List<T> list;

        public InMemoryReader(List<T> list)
        {
            this.list = list;
        }

        public Task<IQueryable<T>> Collection(Func<T, bool> predicate)
            => Task.FromResult((IQueryable<T>)this.list.Where(predicate));

        public Task<Maybe<T>> Single(Func<T, bool> predicate, Func<IEnumerable<T>, T> reduce)
            => Task.FromResult<Maybe<T>>(reduce(this.list.Where(predicate)));
    }
}