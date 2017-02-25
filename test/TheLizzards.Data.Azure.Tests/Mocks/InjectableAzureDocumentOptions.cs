using Microsoft.Extensions.Options;
using TheLizzards.Data.Azure.Entities;

namespace TheLizzards.Data.Azure.Tests.Mocks
{
	internal sealed class InjectableAzureDocumentOptions : IOptions<AzureDocumentDbOptions>
	{
		public AzureDocumentDbOptions Value => new AzureDocumentDbOptions
		{
			AuthKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
			Endpoint = "https://localhost:8081/",
			Databases = new[]
			{
				new AzureDatabase("Test", new []{ "TestItem" })
			},
		};

		public void Deconstruct(out string authKey, out string endpoint)
		{
			authKey = Value.AuthKey;
			endpoint = Value.Endpoint;
		}
	}
}