using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD.Contracts;

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

		public Task Insert(T item)
		{
			throw new NotImplementedException();
		}

		public Task Update(T item)
		{
			throw new NotImplementedException();
		}
	}
}