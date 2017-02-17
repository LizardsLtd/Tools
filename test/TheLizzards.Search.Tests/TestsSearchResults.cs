using TheLizzards.Search.Entities;

namespace TheLizzards.Search.Tests
{
	internal sealed class SearchResultsPayload : ISearchResult
	{
		public double Score { get; set; }
	}
}