﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;

namespace Picums.Data.InMemory
{
    internal class InMemoryWriter<T> : IDataWriter<T> where T : IAggregateRoot
    {
        private List<T> list;

        public InMemoryWriter(List<T> list)
        {
            this.list = list;
        }

        public Task InsertNew(T item)
        {
            this.list.Add(item);
            return Task.CompletedTask;
        }

        public Task UpdateExisting(T item)
        {
            var selectedItem = this.list.First(x => x.Id == item.Id);

            this.list.Remove(selectedItem);

            return this.InsertNew(item);
        }
    }
}