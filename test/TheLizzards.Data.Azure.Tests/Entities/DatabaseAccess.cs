using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Azure.Documents.Client;
using TheLizzards.Data.Azure.Tests.Mocks;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Tests;
using Xunit;

namespace TheLizzards.Data.Azure.Tests.Entities
{
	public sealed class DatabaseAccess : IDisposable
	{
		private readonly AzureDocumentDbContext context;
		private readonly DatabaseParts parts;

		public DatabaseAccess()
		{
			this.context = new AzureDocumentDbContext(new InjectableAzureDocumentOptions(), new TestLoggerFactory());
			this.parts = new DatabaseParts("Test", "TestItem");
		}

		[Fact(Skip = true)]
		public async Task SaveDataWithDataWriter()
		{
			await InitialiseDatabase();
			var writer = this.context.GetWriter<TestItem>(this.parts);
			var item = new TestItem();

			await writer.InsertNew(item);

			var returnItem = LoadItem(item.Id);
			returnItem.Should().NotBeNull();
		}

		public void Dispose()
		{
			DropTestDatabase().GetAwaiter().GetResult();
		}

		private async Task InitialiseDatabase()
					=> await new AzureDocumentDbContextInitialiser(
					new InjectableAzureDocumentOptions()
					, new TestLoggerFactory())
				.Initialise();

		private async Task DropTestDatabase()
		{
			(var authkey, var endpoint) = new InjectableAzureDocumentOptions();
			var client = new DocumentClient(new Uri(endpoint), authkey);

			var databaseLink = UriFactory.CreateDatabaseUri("Test");
			await client.DeleteDatabaseAsync(databaseLink);
		}

		private async Task<TestItem> LoadItem(Guid id)
			=> await this.context
				.GetReader<TestItem>(this.parts)
				.QueryFor(collection => collection.Single(item => item.Id == id));
	};
}