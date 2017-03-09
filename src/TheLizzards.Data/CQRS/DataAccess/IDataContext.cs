using System;
using TheLizzards.Data.Domain;

namespace TheLizzards.Data.CQRS.DataAccess
{
	public interface IDataContext : IDisposable
	{
		IDataReader<T> GetReader<T>(params object[] attributes)
			where T : IAggregateRoot;

		IDataWriter<T> GetWriter<T>(params object[] attributes)
			 where T : IAggregateRoot;
	}
}