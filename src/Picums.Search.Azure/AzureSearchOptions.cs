using System.Linq;
using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;

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

        public static void FromConfiguration(IConfiguration config, AzureSearchOptions options)
        {
            options.ApiKey = new SearchCredentials(config["search:apikey"]);
            options.ServiceName = config["search:service"];
            options.IndexName = config["search:index"];
            options.SearchParameters
                = config
                    .GetSection("search:search-parameter")
                    .GetChildren()
                    .Select(x => x.Value).ToArray();
        }

        public ISearchIndexClient GetSearchIndexClient() => new SearchIndexClient(this.ServiceName, this.IndexName, this.ApiKey);
    }
}