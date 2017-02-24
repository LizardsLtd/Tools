using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.CQRS.Entities;
using TheLizzards.Data.DDD.Contracts;
using TheLizzards.Maybe;

namespace TheLizzards.Data.Tests.CQRS.Contracts.DataAccess
{
	internal class TestDataReader<T> : IDataReader<T>
		where T : IAggregateRoot
	{
		private InMemoryDataStorage inMemoryDataStorage;

		public TestDataReader(InMemoryDataStorage inMemoryDataStorage)
		{
			this.inMemoryDataStorage = inMemoryDataStorage;
		}

		public Task<T> First(Expression<Func<T, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<Maybe<T>> FirstOrDefault(Expression<Func<T, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<T>> GetPage(Expression<Func<T, bool>> predicate, Page page)
		{
			throw new NotImplementedException();
		}

		public Task<T> Single(Expression<Func<T, bool>> predicate)
		{
			var typeName = typeof(T).Name;
			var func = predicate.Compile();

			return Task.FromResult(inMemoryDataStorage[typeName]
				.Cast<T>()
				.Single(func));
		}

		public Task<Maybe<T>> SingleOrDefault(Expression<Func<T, bool>> predicate)
		{
			var typeName = typeof(T).Name;
			var func = predicate.Compile();

			return Task.FromResult(inMemoryDataStorage[typeName]
				.Cast<T>()
				.SingleOrNothing(func));
		}
	}
}