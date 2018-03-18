using Picums.Data.CQRS.DataAccess;

namespace Picums.Data.CQRS.Queries
{
    public interface IWithDataContext<TResult>
    {
        TResult WithDataContext(IDataContext dataContext);
    }
}