using TheLizzards.Data.CQRS.Contracts.DataAccess;

namespace TheLizzards.Data.CQRS.Queries
{
	public interface IWithDataContext<TResult>
	{
		TResult WithDataContext(IDataContext dataContext);
	}
}