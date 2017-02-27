using TheLizzards.Data.CQRS.DataAccess;

namespace TheLizzards.Data.CQRS.Queries
{
	public interface IWithDataContext<TResult>
	{
		TResult WithDataContext(IDataContext dataContext);
	}
}