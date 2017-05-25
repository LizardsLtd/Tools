using System;
using Picums.Data.Domain;

namespace Picums.Data.CQRS.DataAccess
{
    public interface IDataContext : IDisposable
    {
        IDataReader<T> GetReader<T>(params object[] attributes)
            where T : IAggregateRoot;

        IDataWriter<T> GetWriter<T>(params object[] attributes)
             where T : IAggregateRoot;
    }
}