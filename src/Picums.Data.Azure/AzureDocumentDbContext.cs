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
        private readonly AzureDocumentDbOptions options;
        private readonly Lazy<DocumentClient> client;
        private readonly ILogger logger;
        private bool disposedValue;

        public AzureDocumentDbContext(IOptions<AzureDocumentDbOptions> options, ILoggerFactory loggerFactory)
        {
            this.options = options.Value;
            this.client = new Lazy<DocumentClient>(this.options.GetDocumentClient);
            this.logger = loggerFactory.CreateLogger<AzureDocumentDbContext>();
        }

        private bool IsClientCreated => this.client?.IsValueCreated ?? false;

        public IDataReader<T> GetReader<T>()
            where T : IAggregateRoot
        {
            this.logger.LogDebug($"AzudeDocumentDB: TypeOfArgumnet: ${typeof(T).Name}");

            (var databaseId, var collectionId) = this.options.GetDatabaseConfig<T>();
            this.logger.LogDebug($"AzudeDocumentDB: Datebase: ${databaseId}");
            this.logger.LogDebug($"AzudeDocumentDB: CollectionId: ${collectionId}");

            var collectionUri = this.GetCollectionUri(databaseId, collectionId);

            this.logger.LogInformation(
                $"AzureDocumentDb: Reader for {typeof(T).Name} and collection {collectionUri}");

            return new AzureDocumentDbDataReader<T>(
                this.client.Value,
                collectionUri,
                this.logger);
        }

        public IDataWriter<T> GetWriter<T>()
            where T : IAggregateRoot
        {
            (var databaseId, var collectionId) = this.options.GetDatabaseConfig<T>();
            this.logger.LogDebug($"AzudeDocumentDB: Datebase: ${databaseId}");
            this.logger.LogDebug($"AzudeDocumentDB: CollectionId: ${collectionId}");

            this.logger.LogInformation(
                $"AzureDocumentDb: Writer for {typeof(T).Name} and collection {collectionId}");

            return new AzureDocumentDbDataWriter<T>(
                this.client.Value,
                databaseId,
                collectionId,
                this.logger);
        }

        public void Dispose()
        {
            this.logger.LogInformation("AzureDocumentDb: Disposing");

            this.Dispose(true);
        }

        private Uri GetCollectionUri(string databaseId, string collectionId)
            => UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue && this.IsClientCreated)
            {
                lock (this.client)
                {
                    if (!this.disposedValue && this.IsClientCreated)
                    {
                        if (disposing)
                        {
                            this.client.Value.Dispose();
                        }

                        this.disposedValue = true;
                    }
                }
            }
        }
    }
}