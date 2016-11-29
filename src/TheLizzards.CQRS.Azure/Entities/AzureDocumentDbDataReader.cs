using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using TheLizzards.Common.Data;

namespace TheLizzards.CQRS.Azure.Entities
{
	internal sealed class AzureDocumentDbDataReader<T>
		: AzureDocumentDbHandler, IDataReader<T>
			where T : IAggregateRoot
	{
		private readonly DocumentClient client;
		private readonly Uri collectionUri;
		private readonly ILogger<T> logger;

		public AzureDocumentDbDataReader(
						DocumentClient client
				, Uri collectionUri
				, LoggerFactory loggingFactory) :
			base(client, collectionUri, loggingFactory.CreateLogger<T>())
		{
		}

		public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
			=> this.ExecuteCollectionResultQuery<T>(
				predicate
				, items => items);

		public Task<IEnumerable<T>> GetPage(Expression<Func<T, bool>> predicate, Page page)
			=> this.ExecuteCollectionResultQuery<T>(
				predicate
				, items => items
					.Skip(page.CountOfItemsToSkip)
					.Take(page.ItemsOnPage));

		public Task<T> First(Expression<Func<T, bool>> predicate)
			=> this.ExecuteSingleResultQuery<T>(
				predicate
				, items => items.First());

		public Task<Maybe<T>> FirstOrDefault(Expression<Func<T, bool>> predicate)
			=> this.ExecutePossibleSingleResultQuery<T>(
				predicate
				, items => items.FirstOrDefault());

		public Task<T> Single(Expression<Func<T, bool>> predicate)
			=> this.ExecuteSingleResultQuery<T>(
				predicate
				, items => items.Single());

		public Task<Maybe<T>> SingleOrDefault(Expression<Func<T, bool>> predicate)
			=> this.ExecutePossibleSingleResultQuery<T>(
				predicate
				, items => items.SingleOrDefault());
	}
}