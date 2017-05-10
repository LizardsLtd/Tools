using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;
using Picums.Maybe;

namespace Picums.Data.InMemory
{
    public sealed class InMemoryReader<T> : IDataReader<T> where T : IAggregateRoot
    {
        private List<T> list;

        public InMemoryReader(List<T> list)
        {
            this.list = list;
        }

        public Task<IEnumerable<T>> All() => Task.FromResult(this.list.AsEnumerable());

        public Task<TResult> QueryFor<TResult>(Expression<Func<IQueryable<T>, TResult>> predicate)
            => Task.FromResult(predicate.Compile().Invoke(this.list.AsQueryable()));

        public Task<Maybe<T>> SingleOrDefault(Expression<Func<T, bool>> predicate)
            => Task.FromResult(this.list.SingleOrNothing(predicate.Compile()));

        public Task<IQueryable<T>> Where(Expression<Func<T, bool>> predicate)
            => Task.FromResult(this.list.Where(predicate.Compile()).AsQueryable());
    }
}