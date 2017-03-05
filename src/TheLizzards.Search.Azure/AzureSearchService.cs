using System;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Logging;
using TheLizzards.Search.Azure.KeyWords;

namespace TheLizzards.Search.Azure.Services
{
	public abstract class AzureSearchService<T>
		where T : ISearchResult
	{
		private readonly ILogger<AzureSearchService<T>> logger;
		private readonly AzureSearchOptions options;

		public AzureSearchService(ILoggerFactory loggerFactory, AzureSearchOptions options)
		{
			this.options = options;
			this.logger = loggerFactory.CreateLogger<AzureSearchService<T>>();
		}

		public async Task<SearchResults<T>> SearchFor(ISearchForParameter keyword)
		{
			var results = new SearchResults<T>();
			try
			{
				var indexClient = new SearchIndexClient(this.options.ServiceName, this.options.IndexName, this.options.ApiKey);

				var parameters =
					new SearchParameters()
					{
						Select = this.options.SearchParameters
					};

				var documentResults = await indexClient.Documents.SearchAsync(keyword.GetSearchCommmand(), parameters);

				results = ConvertDocumentSearchResult(documentResults);
			}
			catch (Exception exp)
			{
				this.logger.LogError(new EventId(exp.GetHashCode()), exp, "SearchService has failed.");
			}

			return results;
		}

		protected abstract SearchResults<T> ConvertDocumentSearchResult(DocumentSearchResult searchResult);
	}
}