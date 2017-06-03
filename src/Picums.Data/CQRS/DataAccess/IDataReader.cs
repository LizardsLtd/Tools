using Picums.Data.Domain;

namespace Picums.Data.CQRS.DataAccess
{
    public interface IDataReader<TSource> : ICollectionDataReader<TSource>, ISingleItemDataReader<TSource>
        where TSource : IAggregateRoot
    {
    }
}