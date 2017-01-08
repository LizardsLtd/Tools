using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheLizzards.Search.Entities;
using TheLizzards.Search.Services;

namespace TheLizzards.Search.Azure.Services
{
	public abstract class AzureSearchService<T> : ISearchService<T>
		where T : ISearchResult
	{
		private const string ServiceName = "picums";
		private readonly SearchCredentials ApiKey = new SearchCredentials("0FB97B382F5032BF05EA684B4A694983");

		public async Task<SearchResults<T>> SearchFor(IKeyWord keyword)
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

				var documentResults = await indexClient.Documents.SearchAsync(keyword.Combine(), parameters);

				results = ConvertDocumentSearchResult(documentResults);

			}
			catch (Exception exp)
			{
				return new SearchResults<T>();
			}

			return results;
		}

		protected abstract SearchResults<T> ConvertDocumentSearchResult(DocumentSearchResult searchResult);
	}

	//internal static class SearchResultConverter
	//{
	//	public static SearchResults<T> ToSearchResult<T>(this DocumentSearchResult results)
	//			where T : ISearchResult
	//		=> results
	//			.Results
	//			.OrderBy(x => x.Score)
	//			.Select(x=>new SearchR)

	//}
}
