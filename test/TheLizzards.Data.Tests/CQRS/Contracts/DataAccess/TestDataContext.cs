﻿using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.Tests.CQRS.Contracts.DataAccess
{
	internal sealed class TestDataContext : IDataContext
	{
		public TestDataContext(InMemoryDataStorage data)
		{
			this.Data = data;
		}

		public InMemoryDataStorage Data { get; }

		public void Dispose()
		{
		}

		public IDataReader<T> Read<T>(params object[] attributes) where T : IAggregateRoot
			=> new TestDataReader<T>(this.Data);

		public IDataWriter<T> Write<T>(params object[] attributes) where T : IAggregateRoot
			=> new TestDataWriter<T>(this.Data);
	}
}