using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Logging;
using Picums.Search.Azure.KeyWords;

namespace Picums.Search.Azure.Services
{
    public abstract class AzureSearchService<TResultItem, TFactory>
        where TResultItem : IHasScore
        where TFactory : ISearchResultFactory<TResultItem>, new()
    {
        private readonly ILogger<AzureSearchService<TResultItem, TFactory>> logger;
        private readonly TFactory factory;
        private readonly AzureSearchOptions options;

        protected AzureSearchService(ILoggerFactory loggerFactory, AzureSearchOptions options)
        {
            this.options = options;
            this.logger = loggerFactory.CreateLogger<AzureSearchService<TResultItem, TFactory>>();
            this.factory = new TFactory();
        }

        public async Task<SearchResults<TResultItem>> SearchFor(ISearchForParameter keyword)
        {
            try
            {
                var indexClient = new SearchIndexClient(this.options.ServiceName, this.options.IndexName, this.options.ApiKey);
                var parameters = this.BuildSearchParameter();

                var documentResults = await indexClient.Documents.SearchAsync(keyword.GetSearchCommmand(), parameters);

                return new SearchResults<TResultItem>(CreateSearchResults(documentResults));
            }
            catch (Exception exp)
            {
                this.logger.LogError(new EventId(exp.GetHashCode()), exp, "SearchService has failed.");
            }

            return SearchResults<TResultItem>.Empty;
        }

        private SearchParameters BuildSearchParameter()
            => new SearchParameters()
            {
                Select = this.factory.Fields
            };

        private IEnumerable<TResultItem> CreateSearchResults(DocumentSearchResult searchResult)
            => searchResult
                .Results
                .Select(x => this.factory.Create(x.Score, x.Document));
    }
}