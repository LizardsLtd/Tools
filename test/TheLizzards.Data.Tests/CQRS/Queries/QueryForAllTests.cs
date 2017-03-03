using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.CQRS.Queries;
using TheLizzards.Data.Tests.CQRS.Contracts.DataAccess;
using TheLizzards.Data.Tests.Mocks;
using TheLizzards.Tests;
using Xunit;

namespace TheLizzards.Data.Tests.CQRS.Queries
{
	public sealed class QueryForAllTests
	{
		private readonly DatabaseParts parts;
		private readonly TestDataContext context;
		private readonly Guid id;

		public QueryForAllTests()
		{
			this.parts = new DatabaseParts("test", "test");
			this.id = Guid.NewGuid();
			var storage = new InMemoryDataStorage
			{
				["SimpleAggregateRoot"] = new List<object>
				{
					new SimpleAggregateRoot(id)
				}
			};
			this.context = new TestDataContext(storage);
		}

		[Fact]
		public async Task QueringForExistingEntity()
		{
			var query = new QueryForAll<SimpleAggregateRoot>(
					this.context
					, new TestLoggerFactory()
					, this.parts);

			var result = await query.Execute();

			result.Count().Should().Be(1);
		}
	}
}