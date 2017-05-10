using System.Collections.Generic;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;

namespace Picums.Data.InMemory
{
    public sealed class InMemoryDataContext : IDataContext
    {
        private readonly Dictionary<string, object> items;

        public InMemoryDataContext()
        {
            this.items = new Dictionary<string, object>();
        }

        public void Dispose()
        {
        }

        public IDataReader<T> GetReader<T>(params object[] attributes) where T : IAggregateRoot
            => new InMemoryReader<T>(this.GetItemsCollection<T>());

        public IDataWriter<T> GetWriter<T>(params object[] attributes) where T : IAggregateRoot
            => new InMemoryWriter<T>(this.GetItemsCollection<T>());

        public List<T> GetItemsCollection<T>() where T : IAggregateRoot
        {
            var key = this.CreateKey<T>();

            this.CreateCollectionIfKeyNotExist<T>(key);

            return this.items[key] as List<T>;
        }

        private void CreateCollectionIfKeyNotExist<T>(string key)
        {
            if (!this.items.ContainsKey(key))
            {
                this.items[key] = new List<T>();
            }
        }

        private string CreateKey<T>() where T : IAggregateRoot
            => nameof(T);
    }
}