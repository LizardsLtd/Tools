using System.Collections.Generic;
using TheLizzards.Data.Tests.CQRS.Contracts.DataAccess;
using Xunit;

namespace TheLizzards.Data.Tests.Queries
{
	public sealed class AbilityToCreateQuickQuery
	{
		[Fact]
		public void CanCreateQuery()
		{
			var storage = new TestDataContext(new InMemoryDataStorage
			{
				"SampleAggregateRoot" = new List<object>
				{
				}
			});
		}
	}
}