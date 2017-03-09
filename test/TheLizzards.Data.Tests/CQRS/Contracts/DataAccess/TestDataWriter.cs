using System;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.Domain;

namespace TheLizzards.Data.Tests.CQRS.Contracts.DataAccess
{
	internal class TestDataWriter<T> : IDataWriter<T>
		where T : IAggregateRoot
	{
		private InMemoryDataStorage inMemoryDataStorage;

		public TestDataWriter(InMemoryDataStorage inMemoryDataStorage)
		{
			this.inMemoryDataStorage = inMemoryDataStorage;
		}

		public Task InsertNew(T item)
		{
			throw new NotImplementedException();
		}

		public Task UpdateExisting(T item)
		{
			throw new NotImplementedException();
		}
	}
}