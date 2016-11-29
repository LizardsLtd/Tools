using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using TheLizzards.Common.Data;

namespace TheLizzards.CQRS.Azure.Entities
{
	internal sealed class AzureDocumentDbDataReader<T> : IDataReader<T> where T : IAggregateRoot
	{
		private readonly DocumentClient client;
		private readonly Uri collectionUri;

		public AzureDocumentDbDataReader(DocumentClient client, Uri collectionUri)
		{
			this.client = client;
			this.collectionUri = collectionUri;
		}

		public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
		{
			var items = this
				.QueryDocumentDb(predicate)
				.Materialize();

			return Task.FromResult(items);
		}

		public Task<IEnumerable<T>> GetPage(Expression<Func<T, bool>> predicate, Page page)
		{
			var items = this
					.QueryDocumentDb(predicate)
					.Skip(page.CountOfItemsToSkip)
					.Take(page.ItemsOnPage)
					.Materialize();

			return Task.FromResult(items);
		}

		public Task<T> First(Expression<Func<T, bool>> predicate)
		{
			var item = this.client
			.CreateDocumentQuery<T>(collectionUri)
			.First(predicate);

			return Task.FromResult(item);
		}

		public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
		{
			var item = this.client
				.CreateDocumentQuery<T>(collectionUri)
				.FirstOrDefault(predicate);

			return Task.FromResult(item);
		}

		public Task<T> Single(Expression<Func<T, bool>> predicate)
		{
			var item = this.client
				.CreateDocumentQuery<T>(collectionUri)
				.Single(predicate);

			return Task.FromResult(item);
		}

		public Task<Maybe<T>> SingleOrDefault(Expression<Func<T, bool>> predicate)
		{
			var item = this
				.QueryDocumentDb(predicate)
				.SingleOrDefault();

			return Task.FromResult<Maybe<T>>(item);
		}

		private IQueryable<T> QueryDocumentDb(Expression<Func<T, bool>> predicate)
			=> client
				.CreateDocumentQuery<T>(collectionUri)
				.Where(predicate);
	}
}