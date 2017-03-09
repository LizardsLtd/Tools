using Microsoft.Azure.Search;

namespace TheLizzards.Search.Azure
{
	public sealed class AzureSearchOptions
	{
		public AzureSearchOptions()
		{
		}

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

		public SearchCredentials ApiKey { get; set; }

		public string ServiceName { get; set; }

		public string IndexName { get; set; }

		public string[] SearchParameters { get; set; }
	}
}