using Microsoft.Extensions.Logging;

namespace TheLizzards.Data.CQRS.Queries
{
	public interface IWithLogger<TResult>
	{
		TResult WithLogger(ILoggerFactory loggerFactory);
	}
}