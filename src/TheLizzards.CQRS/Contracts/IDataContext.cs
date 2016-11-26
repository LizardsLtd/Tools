using System;
using TheLizzards.Common.Data;

namespace TheLizzards.CQRS
{
	public interface IDataContext : IDisposable
	{
		IDataReader<T> Read<T>(params object[] attributes)
			where T : IAggregateRoot;

		IDataWriter<T> Write<T>(params object[] attributes)
			 where T : IAggregateRoot;
	}
}