using Picums.Search.Azure.KeyWords;

namespace Picums.Search.Azure.Services
{
    public interface ISearchFilter<TResultItem> : ISearchRunner<TResultItem>
        where TResultItem : IHasScore
    {
        ISearchRunner<TResultItem> Filter(IFilter filter);
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