using TheLizzards.Data.CQRS.Contracts.DataAccess;

namespace TheLizzards.Data.CQRS.Queries
{
	public interface IWithDatabaseParts<TResult>
	{
		TResult WithDatabaseParts(DatabaseParts parts);
	}
}