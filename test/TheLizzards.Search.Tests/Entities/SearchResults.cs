using System;
using TheLizzards.Search.Entities;
using Xunit;

namespace TheLizzards.Search.Tests.Entities
{
	public class SearchResults
	{
		[Fact]
		public void CreateEmptyResultsSet()
		{
			var results = new SearchResults<SearchResultsPayload>();

			Assert.False(results.HasResults);
		}

		[Fact]
		public void CreateNonEmptyResults()
		{
			var results = new SearchResults<SearchResultsPayload>(
				new []
				{
					new SearchResultsPayload(),
					new SearchResultsPayload(),
				});

			Assert.True(results.HasResults);
		}
	}
}