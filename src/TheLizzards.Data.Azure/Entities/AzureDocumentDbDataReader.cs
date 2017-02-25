using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.Azure.Entities
{
	internal sealed class AzureDocumentDbDataReader<T> : IDataReader<T>
		where T : IAggregateRoot
	{
		private readonly DocumentClient client;
		private readonly Uri collectionUri;
		private readonly ILogger logger;

		public AzureDocumentDbDataReader(
			DocumentClient client
			, Uri collectionUri
			, ILogger logger)
		{
			this.client = client;
			this.collectionUri = collectionUri;
			this.logger = logger;
		}

		public Task<IEnumerable<T>> All()

		{
			throw new NotImplementedException();
		}

		public Task<TResult> QueryFor<TResult>(Expression<Func<IQueryable<T>, TResult>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<T>> Where(Expression<Func<T, bool>> predicate)
		{
			throw new NotImplementedException();
		}

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