using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;
using Picums.Maybe;

namespace Picums.Data.Azure
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
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} All function");
            return Task.FromResult<IEnumerable<T>>(this.client.CreateDocumentQuery<T>(this.collectionUri).ToArray());
        }

        public Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
        {
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} Where function");
            var items = this.client
                .CreateDocumentQuery<T>(this.collectionUri)
                .Where(predicate.Compile())
                .ToArray();

            return Task.FromResult<IEnumerable<T>>(items);
        }

        public Task<Maybe<T>> SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} SingleOrDefault function");
            var result = this.client
                .CreateDocumentQuery<T>(this.collectionUri, new FeedOptions { MaxItemCount = 2 })
                .SingleOrDefault(predicate.Compile());

            return Task.FromResult<Maybe<T>>(result);
        }

        public Task<Maybe<T>> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} FirstOrDefault function");
            var result = this.client
                .CreateDocumentQuery<T>(this.collectionUri, new FeedOptions { MaxItemCount = 1 })
                .FirstOrDefault(predicate.Compile());

            return Task.FromResult<Maybe<T>>(result);
        }

        public Task<Maybe<T>> ById(Guid id)
        {
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} FirstOrDefault function");

            var feedOptions = new FeedOptions { MaxItemCount = 1 };
            var byIdSqlStatement = this.GetSqlToQueryById(id);

            var result = this.client
                .CreateDocumentQuery<T>(this.collectionUri, byIdSqlStatement, feedOptions)
                .SingleOrDefault();

            return Task.FromResult<Maybe<T>>(result);
        }

        private string GetSqlToQueryById(Guid id)
            => $"SELECT * FROM items WHERE items.id = {id}";
    }
}