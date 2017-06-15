using System.Collections.Generic;
using System.Linq;

namespace Picums.Search.Azure
{
    public sealed class SearchResults<TResultItem>
        where TResultItem : IHasScore
    {
        public static SearchResults<TResultItem> Empty = new SearchResults<TResultItem>(new TResultItem[0]);

        public SearchResults(IEnumerable<TResultItem> results)
        {
            this.Results = results.ToArray();
        }

        public TResultItem[] Results { get; }

        public bool HasResults => this.Results.Any();

        public SearchResults<TResultItem> Merge(SearchResults<TResultItem> searchResults)
            => new SearchResults<TResultItem>(this
                .Results
                .Union(searchResults?.Results));
    }
}