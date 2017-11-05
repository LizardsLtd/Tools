using Picums.Data.CQRS.DataAccess;

namespace Picums.Data.CQRS.Queries
{
    public interface IWithConfiguration<TResult>
    {
        TResult WithConfiguration(IDatabaseConfiguration configuration);
    }
}