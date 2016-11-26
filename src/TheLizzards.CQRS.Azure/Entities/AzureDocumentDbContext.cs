using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TheLizzards.Common.Data;

namespace TheLizzards.CQRS.Azure.Entities
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

		public IDataReader<T> Read<T>(params object[] attributes) where T : IAggregateRoot
		{
			var collectionUri = GetCollectionUri(attributes);

			this.logger.LogInformation(
				$"AzureDocumentDb: Reader for {typeof(T).Name} and collection {collectionUri}");

			return new AzureDocumentDbDataReader<T>(this.client.Value, collectionUri);
		}

		public IDataWriter<T> Write<T>(params object[] attributes) where T : IAggregateRoot
		{
			var databaseId = attributes[0].ToString();
			var collectionId = attributes[1].ToString();

			this.logger.LogInformation(
				$"AzureDocumentDb: Writer for {typeof(T).Name} and collection {collectionId}");

			return new AzureDocumentDbDataWriter<T>(this.client.Value, databaseId, collectionId);
		}

		public void Dispose()
		{
			this.logger.LogInformation("AzureDocumentDb: Disposing");

			Dispose(true);
		}

		private Uri GetCollectionUri(object[] attributes)
		{
			var databaseId = attributes[0].ToString();
			var collectionId = attributes[1].ToString();
			return UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);
		}

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