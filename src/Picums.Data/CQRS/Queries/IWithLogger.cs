using NLog;

namespace Picums.Data.CQRS.Queries
{
    public interface IWithLogger<TResult>
    {
        TResult WithLogger(ILogger logger);
    }
}