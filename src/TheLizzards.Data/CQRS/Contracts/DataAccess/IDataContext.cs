using System;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.CQRS.Contracts.DataAccess
{
	public interface IDataContext : IDisposable
	{
		IDataReader<T> Read<T>(params object[] attributes)
			where T : IAggregateRoot;

		IDataWriter<T> Write<T>(params object[] attributes)
			 where T : IAggregateRoot;
	}
}