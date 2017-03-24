using System.Collections.Generic;
using System.Linq;

namespace Picums.Search.Azure
{
	public sealed class SearchResults<T>
		where T : ISearchResult
	{
		public SearchResults()
			: this(new T[0])
		{
		}

		public SearchResults(IEnumerable<T> results)
		{
			this.Results = results.ToArray();
		}

		public T[] Results { get; }

		public bool HasResults => this.Results.Any();

		public SearchResults<T> Merge(SearchResults<T> searchResults)
			=> new SearchResults<T>(this
				.Results
				.Union(searchResults?.Results));
	}
}