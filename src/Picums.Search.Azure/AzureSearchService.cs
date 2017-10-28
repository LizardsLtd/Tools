using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using NLog;
using Picums.Maybe;
using Picums.Search.Azure.KeyWords;

namespace Picums.Search.Azure.Services
{
    public sealed class AzureSearchService<TResultItem, TFactory>
            : ISearchBuilder<TResultItem>
            , ISearchFilter<TResultItem>
            , ISearchRunner<TResultItem>
        where TResultItem : IHasScore
        where TFactory : ISearchResultFactory<TResultItem>, new()
    {
        private readonly ISearch searchFor;
        private readonly Maybe<IFilter> filter;
        private readonly AzureSearchOptions options;
        private readonly Lazy<TFactory> factory;
        private readonly ILogger logger;

        public AzureSearchService(ILogger logger, AzureSearchOptions options)
        {
            this.logger = logger;
            this.options = options;
            this.factory = new Lazy<TFactory>(() => new TFactory(), true);
        }

        private AzureSearchService(
                ILogger logger
                , AzureSearchOptions options
                , ISearch searchFor)
            : this(logger, options)
        {
            this.searchFor = searchFor;
        }

        private AzureSearchService(
                ILogger logger
                , AzureSearchOptions options
                , ISearch searchFor
                , IFilter filter)
            : this(logger, options, searchFor)
        {
            this.filter = Maybe<IFilter>.Some(filter);
        }

        public ISearchFilter<TResultItem> SearchFor(ISearch searchFor)
            => new AzureSearchService<TResultItem, TFactory>(this.logger, this.options, searchFor);

        public ISearchRunner<TResultItem> Filter(IFilter filter)
            => new AzureSearchService<TResultItem, TFactory>(this.logger, this.options, this.searchFor, filter);

        public async Task<SearchResults<TResultItem>> RunSearchAsync()
        {
            try
            {
                var indexClient = this.options.GetSearchIndexClient();
                var parameters = this.BuildSearchParameter();

                var documentResults = await indexClient.Documents.SearchAsync(this.searchFor.GetSearchText(), parameters);

                return new SearchResults<TResultItem>(CreateSearchResults(documentResults));
            }
            catch (Exception exp)
            {
                this.logger.Error(exp, "SearchService has failed.");
            }

            return SearchResults<TResultItem>.Empty;
        }

        private SearchParameters BuildSearchParameter()
        {
            if (this.filter.IsSome)
            {
                return new SearchParameters()
                {
                    Select = this.factory.Value.Fields,
                    Filter = this.filter.Value.GetFilter()
                };
            }
            else
            {
                return new SearchParameters()
                {
                    Select = this.factory.Value.Fields,
                };
            }
        }

        private IEnumerable<TResultItem> CreateSearchResults(DocumentSearchResult searchResult)
            => searchResult
                .Results
                .Select(x => this.factory.Value.Create(x.Score, x.Document));
    }
}