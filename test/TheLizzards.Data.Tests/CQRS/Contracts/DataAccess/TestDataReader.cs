using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;

namespace TheLizzards.Data.Tests.CQRS.Contracts.DataAccess
{
	internal class TestDataReader<T> : IDataReader<T>
		where T : IAggregateRoot
	{
		private InMemoryDataStorage inMemoryDataStorage;

		public TestDataReader(InMemoryDataStorage inMemoryDataStorage)
		{
			this.inMemoryDataStorage = inMemoryDataStorage;
			this.NameOfType = typeof(T).Name;
		}

		private string NameOfType { get; }

		public Task<IEnumerable<T>> All() => Task.FromResult(CurrentResults());

		public Task<TResult> QueryFor<TResult>(Expression<Func<IQueryable<T>, TResult>> predicate)
			=> Task.FromResult(predicate.Compile().Invoke(CurrentResults().AsQueryable()));

		public Task<IQueryable<T>> Where(Expression<Func<T, bool>> predicate)
			=> Task.FromResult(CurrentResults().Where(predicate.Compile()).AsQueryable());

		private IEnumerable<T> CurrentResults()
			=> this.inMemoryDataStorage[this.NameOfType].Cast<T>();
	}
}