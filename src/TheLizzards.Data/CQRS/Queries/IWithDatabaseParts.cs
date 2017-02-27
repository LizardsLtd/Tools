using TheLizzards.Data.CQRS.DataAccess;

namespace TheLizzards.Data.CQRS.Queries
{
	public interface IWithDatabaseParts<TResult>
	{
		TResult WithDatabaseParts(DatabaseParts parts);
	}
}