using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Logging;
using Picums.Search.Azure.KeyWords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IFilter filter;
        private readonly AzureSearchOptions options;
        private readonly Lazy<TFactory> factory;
        private readonly ILogger<AzureSearchService<TResultItem, TFactory>> logger;

        public AzureSearchService(ILoggerFactory loggerFactory, AzureSearchOptions options)
            : this(loggerFactory.CreateLogger<AzureSearchService<TResultItem, TFactory>>(), options)
        {
        }

        private AzureSearchService(
                ILogger<AzureSearchService<TResultItem, TFactory>> logger
                , AzureSearchOptions options
                , ISearch searchFor)
            : this(logger, options)
        {
            this.searchFor = searchFor;
        }

        private AzureSearchService(
                ILogger<AzureSearchService<TResultItem, TFactory>> logger
                , AzureSearchOptions options
                , ISearch searchFor
                , IFilter filter)
            : this(logger, options, searchFor)
        {
            this.filter = filter;
        }

        private AzureSearchService(ILogger<AzureSearchService<TResultItem, TFactory>> logger, AzureSearchOptions options)
        {
            this.logger = logger;
            this.options = options;
            this.factory = new Lazy<TFactory>(() => new TFactory(), true);
        }

        public ISearchFilter<TResultItem> SearchFor(ISearch searchFor)
            => new AzureSearchService<TResultItem, TFactory>(this.logger, this.options, searchFor);

        public ISearchRunner<TResultItem> Filter(IFilter filter)
            => new AzureSearchService<TResultItem, TFactory>(this.logger, this.options, this.searchFor, filter);

        public async Task<SearchResults<TResultItem>> RunSearchAsync()
        {
            try
            {
                var indexClient = new SearchIndexClient(this.options.ServiceName, this.options.IndexName, this.options.ApiKey);
                var parameters = this.BuildSearchParameter();

                var documentResults = await indexClient.Documents.SearchAsync(this.searchFor.GetSearchText(), parameters);

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
                Select = this.factory.Value.Fields,
                Filter = this.filter.GetFilter(),
            };

        private IEnumerable<TResultItem> CreateSearchResults(DocumentSearchResult searchResult)
            => searchResult
                .Results
                .Select(x => this.factory.Value.Create(x.Score, x.Document));
    }

    //public abstract class AzureSearchService<TResultItem, TFactory>
    //    where TResultItem : IHasScore
    //    where TFactory : ISearchResultFactory<TResultItem>, new()
    //{
    //    private readonly ILogger<AzureSearchService<TResultItem, TFactory>> logger;
    //    private readonly TFactory factory;
    //    private readonly AzureSearchOptions options;
    //    private readonly SearchParameters parameters;

    //    protected AzureSearchService(ILoggerFactory loggerFactory, AzureSearchOptions options)
    //    {
    //        this.options = options;
    //        this.logger = loggerFactory.CreateLogger<AzureSearchService<TResultItem, TFactory>>();
    //        this.factory = new TFactory();
    //    }

    //    private AzureSearchService(SearchParameters parameters)
    //    {
    //        this.parameters = parameters;
    //    }

    //    public AzureSearchService<TResultItem, TFactory> SearchFor(ISearchForParameter keyword)
    //    {
    //        return this;
    //    }

    //    public AzureSearchService<TResultItem, TFactory> FilterBy()
    //    {
    //        return this;
    //    }

    //    public async Task<SearchResults<TResultItem>> RunAsync()
    //    {
    //        try
    //        {
    //            var indexClient = new SearchIndexClient(this.options.ServiceName, this.options.IndexName, this.options.ApiKey);
    //            var parameters = this.BuildSearchParameter();

    //            var documentResults = await indexClient.Documents.SearchAsync(keyword.GetSearchCommmand(), parameters);

    //            return new SearchResults<TResultItem>(CreateSearchResults(documentResults));
    //        }
    //        catch (Exception exp)
    //        {
    //            this.logger.LogError(new EventId(exp.GetHashCode()), exp, "SearchService has failed.");
    //        }

    //        return SearchResults<TResultItem>.Empty;
    //    }

    //}

    //internal sealed class AzureSearchParameters
    //{
    //    public AzureSearchParameters(IList<string> select)
    //    {
    //        this.Select = select;
    //    }

    //    public AzureSearchParameters(
    //            IList<string> select
    //            , string searchPhraze
    //            , IFilter filter)
    //        : this(select)
    //    {
    //        this.SearchPhraze = searchPhraze;
    //        this.Filter = filter;
    //    }

    //    public IList<string> Select { get; }

    //    public string SearchPhraze { get; }

    //    public IFilter Filter { get; }

    //    public SearchParameters GetParameters()
    //        => new SearchParameters
    //        {
    //            Select = this.Select,
    //            Filter = this.Filter?.GetFilter(),
    //        };
    //}
}