using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;
using Picums.Maybe;

namespace Picums.Data.Tests.CQRS.Contracts.DataAccess
{
    internal class TestDataReader<T> : IDataReader<T>
        where T : IAggregateRoot
    {
        private InMemoryDataStorage inMemoryDataStorage;

        public TestDataReader(InMemoryDataStorage inMemoryDataStorage)
        {
            this.inMemoryDataStorage = inMemoryDataStorage;
            this.NameOfType = typeof(T).Name;
        }

        private string NameOfType { get; }

        public Task<IEnumerable<T>> Collection(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Maybe<T>> Single(Func<T, bool> predicate, Func<IEnumerable<T>, T> reduce)
        {
            throw new NotImplementedException();
        }
    }
}