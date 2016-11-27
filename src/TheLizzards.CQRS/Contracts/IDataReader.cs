using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheLizzards.Common.Data;

namespace TheLizzards.CQRS
{
	public interface IDataReader<T>
		where T : IAggregateRoot
	{
		Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

		Task<IEnumerable<T>> GetPage(Expression<Func<T, bool>> predicate, Page page);

		Task<T> Single(Expression<Func<T, bool>> predicate);

		Task<Maybe<T>> SingleOrDefault(Expression<Func<T, bool>> predicate);
	}
}