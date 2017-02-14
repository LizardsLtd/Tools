using System;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Logging;
using TheLizzards.Search.Entities;
using TheLizzards.Search.Services;

namespace TheLizzards.Search.Azure.Services
{
	public abstract class TextAzureSearchService<T> : ISearchService<TextSearchKeyWord, T>
		where T : ISearchResult
	{
		private const string ServiceName = "picums";
		private readonly SearchCredentials ApiKey = new SearchCredentials("0FB97B382F5032BF05EA684B4A694983");
		private readonly ILogger<ISearchService<TextSearchKeyWord, T>> logger;

		public TextAzureSearchService(ILoggerFactory loggerFactory)
		{
			this.logger = loggerFactory.CreateLogger<ISearchService<TextSearchKeyWord, T>>();
		}

		public async Task<SearchResults<T>> SearchFor(TextSearchKeyWord keyword)
		{
			var results = new SearchResults<T>();
			try
			{
				var indexClient = new SearchIndexClient(ServiceName, "provider-by-details", ApiKey);

				var parameters =
					new SearchParameters()
					{
						Select = new[] { "id", "Name", "Description" }
					};

				var documentResults = await indexClient.Documents.SearchAsync(keyword.SearchTokens, parameters);

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