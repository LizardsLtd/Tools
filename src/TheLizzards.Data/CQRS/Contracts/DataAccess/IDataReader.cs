using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.CQRS.Contracts.DataAccess
{
	public interface IDataReader<T>
		where T : IAggregateRoot
	{
		Task<IEnumerable<T>> All();

		Task<TResult> QueryFor<TResult>(Expression<Func<IQueryable<T>, TResult>> predicate);

		Task<IQueryable<T>> Where(Expression<Func<T, bool>> predicate);
	}
}