using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using TheLizzards.Data.Queries;
using TheLizzards.Data.Tests.CQRS.Contracts.DataAccess;
using TheLizzards.Data.Tests.Mocks;
using TheLizzards.Tests;
using Xunit;

namespace TheLizzards.Data.Tests.Queries
{
	public sealed class AbilityToCreateQuickQuery
	{
		private readonly DatabaseParts parts;
		private readonly TestDataContext context;
		private readonly Guid id;

		public AbilityToCreateQuickQuery()
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
		public async Task SingleQueryWithFailWhenNotFound()
		{
			var query = new SampleQuery(this.context, new NullLoggerFactory(), this.parts, id);

			var result = await query.Execute();

			result.Id.Should().Be(id);
		}

		[Fact]
		public async Task SingleQueryWithMaybe()
		{
			var query = new SampleMaybeQuery(this.context, new NullLoggerFactory(), this.parts, id);

			var result = await query.Execute();

			result.IsSome.Should().Be(true);
			result.Value.Id.Should().Be(id);
		}
	}
}