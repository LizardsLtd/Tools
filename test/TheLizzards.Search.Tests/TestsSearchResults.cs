using System;
using TheLizzards.Search.Entities;
using Xunit;

namespace TheLizzards.Search.Tests
{
	internal sealed class SearchResultsPayload : ISearchResult
	{
		public double Score { get; set; }
	}
}