using Microsoft.Extensions.Logging;

namespace Picums.Data.CQRS.Queries
{
    public interface IWithLogger<TResult>
    {
        TResult WithLogger(ILoggerFactory loggerFactory);
    }
}