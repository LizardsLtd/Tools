using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Picums.Search.Azure
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

        public static void FromConfiguration(IConfigurationRoot root, AzureSearchOptions options)
        {
            options.ApiKey = new SearchCredentials(root["search:apikey"]);
            options.ServiceName = root["search:service"];
            options.IndexName = root["search:index"];
            options.SearchParameters
                = root.GetSection("search:search-parameter").GetChildren().Select(x => x.Value).ToArray();
        }

        public ISearchIndexClient GetSearchIndexClient() => new SearchIndexClient(this.ServiceName, this.IndexName, this.ApiKey);
    }
}