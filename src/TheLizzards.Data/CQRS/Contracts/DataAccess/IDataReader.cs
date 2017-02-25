using System.Linq;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.CQRS.Contracts.DataAccess
{
	public interface IDataReader<T>
		where T : IAggregateRoot
	{
		IQueryable<T> Query();

		//Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

		//Task<IEnumerable<T>> GetPage(Expression<Func<T, bool>> predicate, Page page);

		//Task<T> First(Expression<Func<T, bool>> predicate);

		//Task<Maybe<T>> FirstOrDefault(Expression<Func<T, bool>> predicate);

		//Task<T> Single(Expression<Func<T, bool>> predicate);

		//Task<Maybe<T>> SingleOrDefault(Expression<Func<T, bool>> predicate);
	}
}