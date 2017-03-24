using Picums.Data.CQRS.DataAccess;

namespace Picums.Data.CQRS.Queries
{
	public interface IWithDatabaseParts<TResult>
	{
		TResult WithDatabaseParts(DatabaseParts parts);
	}
}