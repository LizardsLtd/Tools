using System;
using System.Linq;
using Microsoft.Azure.Documents.Client;

namespace TheLizzards.CQRS.Azure.Entities
{
	public sealed class AzureDatabase
	{
		public AzureDatabase(string databaseId, IEnumerable<string> collections)
		{
			this.DatabaseId = databaseId;
			this.Collections = collections.ToArray();
		}
   
		public string DatabaseId { get; }
    
		public string[] Collections{ get; }
	 
		internal void CreateDatabaseWithCollection(DatabaseClient client)
		{
			var database = this.CreateDatabase();
		}
		public void CreateDatabases()
			=> this.databases
				.Select(this.CreateDatabase)
				.Where(DoesDatabaseHasToBeCreated)
				.ToList()
				.ForEach(async db => await this.client.CreateDatabaseAsync(db));

		private bool DoesDatabaseHasToBeCreated(Database database)
			=> !DoesDatabseExist(database);

		private bool DoesDatabseExist(Database database)
			=> client.CreateDatabaseQuery()
				.Where(db => db.Id == database.Id)
				.ToArray()
				.Any();

		private Database CreateDatabase()
			=> new Database
			{
				Id = databaseId,
			};
	}
}
