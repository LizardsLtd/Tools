using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;

namespace Picums.Data.Azure
{
    public sealed class AzureDocumentDbContext : IDataContext
    {
        private readonly Lazy<DocumentClient> client;
        private readonly ILogger logger;
        private bool disposedValue;

        public AzureDocumentDbContext(
            IOptions<AzureDocumentDbOptions> options
            , ILoggerFactory loggerFactory)
        {
            this.client = new Lazy<DocumentClient>(
                () => new DocumentClient(new Uri(options.Value.Endpoint), options.Value.AuthKey));
            this.logger = loggerFactory.CreateLogger<AzureDocumentDbContext>();
        }

        private bool IsClientCreated => this.client?.IsValueCreated ?? false;

        public IDataReader<T> GetReader<T>(params object[] attributes)
            where T : IAggregateRoot
        {
            (var databaseId, var collectionId) = UnwrapDatabaseParts(attributes);

            var collectionUri = GetCollectionUri(databaseId, collectionId);

            this.logger.LogInformation(
                $"AzureDocumentDb: Reader for {typeof(T).Name} and collection {collectionUri}");

            return new AzureDocumentDbDataReader<T>(
                this.client.Value
                , collectionUri
                , this.logger);
        }

        public IDataWriter<T> GetWriter<T>(params object[] attributes)
            where T : IAggregateRoot
        {
            (var databaseId, var collectionId) = UnwrapDatabaseParts(attributes);

            this.logger.LogInformation(
                $"AzureDocumentDb: Writer for {typeof(T).Name} and collection {collectionId}");

            return new AzureDocumentDbDataWriter<T>(
                this.client.Value
                , databaseId
                , collectionId
                , this.logger);
        }

        public void Dispose()
        {
            this.logger.LogInformation("AzureDocumentDb: Disposing");

            Dispose(true);
        }

        private (string, string) UnwrapDatabaseParts(object[] attributes)
        {
            var parts = attributes[0] as DatabaseParts;

            return (parts.Database, parts.Collection);
        }

        private Uri GetCollectionUri(string databaseId, string collectionId)
            => UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

        private void Dispose(bool disposing)
        {
            if (!disposedValue && IsClientCreated)
            {
                lock (this.client)
                {
                    if (!disposedValue && IsClientCreated)
                    {
                        if (disposing)
                        {
                            this.client.Value.Dispose();
                        }

                        disposedValue = true;
                    }
                }
            }
        }
    }
}