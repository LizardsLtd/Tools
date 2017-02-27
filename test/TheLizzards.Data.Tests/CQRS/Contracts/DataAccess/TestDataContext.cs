using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;

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

		public IDataReader<T> GetReader<T>(params object[] attributes) where T : IAggregateRoot
			=> new TestDataReader<T>(this.Data);

		public IDataWriter<T> GetWriter<T>(params object[] attributes) where T : IAggregateRoot
			=> new TestDataWriter<T>(this.Data);
	}
}