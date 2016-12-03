using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		private readonly IEnumerable<AzureDatabase> databases;
		private bool disposedValue;

		public AzureDocumentDbContextInitialiser(
			IOptions<AzureDocumentDbOptions> options
			, ILoggerFactory loggerFactory)
		{
			this.client = new DocumentClient(new Uri(options.Value.Endpoint), options.Value.AuthKey);
			this.databases = options.Value.Databases;
			this.logger = loggerFactory.CreateLogger<AzureDocumentDbContextInitialiser>();
		}

		public void Dispose()
		{
			this.logger.LogInformation("AzureDocumentDb: Disposing");

			Dispose(true);
		}

		public Task Initialise()
			=> Task.Run(()
				=> this.databases
					.ToList()
					.ForEach(azureDb
						=> azureDb.CreateDatabaseWithCollection(client)));

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