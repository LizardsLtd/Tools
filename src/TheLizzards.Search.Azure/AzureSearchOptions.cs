using Microsoft.Azure.Search;

namespace TheLizzards.Search.Azure
{
	public sealed class AzureSearchOptions
	{
		public AzureSearchOptions(
			string apiKey
			, string serviceName
			, string indexName
			, string[] searchParameters)
		{
			this.ApiKey = new SearchCredentials(apiKey);
			this.ServiceName = serviceName;
			this.IndexName = indexName;
			this.SearchParameters = searchParameters;
		}

		public SearchCredentials ApiKey { get; }

		public string ServiceName { get; }

		public string IndexName { get; }

		public string[] SearchParameters { get; }
	}
}