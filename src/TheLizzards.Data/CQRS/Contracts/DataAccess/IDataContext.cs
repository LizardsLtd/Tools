using System;
using TheLizzards.Data.DDD;

namespace TheLizzards.Data.CQRS.Contracts.DataAccess
{
	public interface IDataContext : IDisposable
	{
		IDataReader<T> GetReader<T>(params object[] attributes)
			where T : IAggregateRoot;

		IDataWriter<T> GetWriter<T>(params object[] attributes)
			 where T : IAggregateRoot;
	}
}