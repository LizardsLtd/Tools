using System;

namespace Picums.Data.CQRS.Queries
{
    public interface IWithId<TResult>
    {
        TResult WithId(Guid id);
    }
}