using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TheLizzards.CQRS.Contracts.Data;

namespace TheLizzards.CQRS.Azure.Entities
{
	public sealed class AzureDocumentDbContextInitialiser : IDataContextInitialiser
	{
		private readonly DocumentClient client;
		private readonly ILogger<AzureDocumentDbContextInitialiser> logger;
		private readonly string[] databases;
		private bool disposedValue;

		public AzureDocumentDbContextInitialiser(
			IOptions<AzureDocumentDbOptions> options
			, ILoggerFactory loggerFactory)
		{
			this.client = new DocumentClient(new Uri(options.Value.Endpoint), options.Value.AuthKey);
			this.databases = options.Value.Database.Split(',');
			this.logger = loggerFactory.CreateLogger<AzureDocumentDbContextInitialiser>();
		}

		public void Dispose()
		{
			this.logger.LogInformation("AzureDocumentDb: Disposing");

			Dispose(true);
		}

		public Task Initialise()
			=> Task.Run((Action)CreateDatabases);

		private void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				lock (this.client)
				{
					if (!disposedValue)
					{
						if (disposing)
						{
							this.client.Dispose();
						}

						disposedValue = true;
					}
				}
			}
		}
	}
}
