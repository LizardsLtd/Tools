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
			=> Task.Run(() => client
					.CreateDocumentQuery<T>(collectionUri)
					.Where(predicate)
					.ToList()
					.AsEnumerable());

		public Task<IEnumerable<T>> GetPage(Expression<Func<T, bool>> predicate, Page page)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<T>> GetPage(Func<T, bool> predicate, Page page)
			=> Task.Run(() => client
					.CreateDocumentQuery<T>(collectionUri)
					.Where(predicate)
					.Skip(page.CountOfItemsToSkip)
					.Take(page.ItemsOnPage)
					.ToList()
					.AsEnumerable());

		public Task<T> Single(Expression<Func<T, bool>> predicate)
			=> Task.Run(()
				=> this.client
					.CreateDocumentQuery<T>(collectionUri)
					.Single(predicate));

		public Task<Maybe<T>> SingleOrDefault(Expression<Func<T, bool>> predicate)
			=> Task.Run(() => SingleOrDefaultNonAsync(predicate.Compile()));

		private Maybe<T> SingleOrDefaultNonAsync(Func<T, bool> predicate)
		{
			var item = this.client
				.CreateDocumentQuery<T>(collectionUri)
				.Where(predicate)
		                .ToArray()
				.SingleOrDefault();

			return (Maybe<T>)item;
		}
	}
}
