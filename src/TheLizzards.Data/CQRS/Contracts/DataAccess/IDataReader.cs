using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.Entities;
using TheLizzards.Data.DDD.Contracts;
using TheLizzards.Maybe;

namespace TheLizzards.Data.CQRS.Contracts.DataAccess
{
	public interface IDataReader<T>
		where T : IAggregateRoot
	{
		Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);

		Task<IEnumerable<T>> GetPage(Expression<Func<T, bool>> predicate, Page page);

		Task<T> First(Expression<Func<T, bool>> predicate);

		Task<Maybe<T>> FirstOrDefault(Expression<Func<T, bool>> predicate);

		Task<T> Single(Expression<Func<T, bool>> predicate);

		Task<Maybe<T>> SingleOrDefault(Expression<Func<T, bool>> predicate);
	}
}