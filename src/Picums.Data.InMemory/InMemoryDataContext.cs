using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;
using System;
using System.Collections.Generic;

namespace Picums.Data.InMemory
{
    public sealed class InMemoryDataContext : IDataContext
    {
        private readonly Dictionary<string, List<object>> items;

        public InMemoryDataContext()
        {
            this.items = new Dictionary<string, List<object>>();
        }

        public void Dispose()
        {
        }

        public IDataReader<T> GetReader<T>(params object[] attributes) where T : IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public IDataWriter<T> GetWriter<T>(params object[] attributes) where T : IAggregateRoot
        {
            throw new NotImplementedException();
        }
    }
}