using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace TheLizzards.Data.Azure
{
	public sealed class AzureDatabase
	{
		public AzureDatabase(string databaseId, IEnumerable<string> collections)
		{
			this.DatabaseId = databaseId;
			this.Collections = collections.ToArray();
		}

		public string DatabaseId { get; }

		public string[] Collections { get; }

		internal void CreateDatabaseWithCollection(DocumentClient client)
			=> new AzureDatabaseCreator(client)
				.EnsureDatabseExist(CreateDatabase())
				.EnsureCollectionExists(Collections);

		private Database CreateDatabase()
			=> new Database
			{
				Id = this.DatabaseId,
			};
	}
}