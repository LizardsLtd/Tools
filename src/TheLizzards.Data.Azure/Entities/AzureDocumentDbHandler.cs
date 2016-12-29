using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.Azure.Entities
{
	internal abstract class AzureDocumentDbHandler
	{
		private readonly DocumentClient client;
		private readonly Uri collectionUri;
		private readonly ILogger logger;

		protected AzureDocumentDbHandler(
			DocumentClient client
			, Uri collectionUri
			, ILogger logger)
		{
			this.client = client;
			this.collectionUri = collectionUri;
			this.logger = logger;
		}

		protected Task<TPayload> ExecuteSingleResultQuery<TPayload>(
				Expression<Func<TPayload, bool>> predicate
				, Func<IEnumerable<TPayload>, TPayload> resultsExtractor)
					where TPayload : IAggregateRoot
			=> ExecuteQuery(
				predicate
				, items => resultsExtractor(items.Materialize()));

		protected Task<TResult> ExecuteSingleResultQuery<TPayload, TResult>(
				Expression<Func<TPayload, bool>> predicate
				, Func<IEnumerable<TPayload>, TResult> resultsExtractor)
					where TPayload : IAggregateRoot
			=> ExecuteQuery(
				predicate
				, items => resultsExtractor(items.Materialize()));

		protected Task<IEnumerable<TPayload>> ExecuteCollectionResultQuery<TPayload>(
				Expression<Func<TPayload, bool>> predicate
				, Func<IQueryable<TPayload>, IQueryable<TPayload>> resultsExtractor)
					where TPayload : IAggregateRoot
			=> ExecuteQuery(
				predicate
				, items => resultsExtractor(items).Materialize());

		private Task<TResult> ExecuteQuery<TPayload, TResult>(
			Expression<Func<TPayload, bool>> predicate
			, Func<IQueryable<TPayload>, TResult> resultsExtractor)
				where TPayload : IAggregateRoot
		{
			try
			{
				this.logger.LogInformation(
					$"Azure DocDb: For type {nameof(TPayload)} on collection {this.collectionUri}");

				var items = this.QueryDocumentDb(predicate);

				this.logger.LogInformation($"Items count: {items}");

				var result = resultsExtractor(items);

				return Task.FromResult(result);
			}
			catch (Exception exp)
			{
				this.logger.LogError(
					new EventId(1)
					, exp
					, exp.Message);

				return Task.FromException<TResult>(exp);
			}
		}

		private IQueryable<T> QueryDocumentDb<T>(Expression<Func<T, bool>> predicate)
				where T : IAggregateRoot
			=> client
				.CreateDocumentQuery<T>(collectionUri)
				.Where(predicate);
	}
}