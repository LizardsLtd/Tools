using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.Tests.CQRS.Contracts.DataAccess;
using TheLizzards.Data.Tests.Mocks;
using TheLizzards.Tests;
using Xunit;

namespace TheLizzards.Data.Tests.CQRS.Queries
{
	public sealed class QueryByIdWithDefault
	{
		private readonly DatabaseParts parts;
		private readonly TestDataContext context;
		private readonly Guid id;

		public QueryByIdWithDefault()
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
			var query = new TestMaybeQuery()
				.WithDataContext(this.context)
				.WithLogger(new TestLoggerFactory())
				.WithDatabaseParts(this.parts)
				.WithId(id);

			var result = await query.Execute();

			result.IsSome.Should().Be(true);
			result.Value.Id.Should().Be(id);
		}
	}
}