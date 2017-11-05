using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NLog;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.CQRS.Queries;
using Picums.Data.InMemory;
using Picums.Data.Tests.Mocks;
using Xunit;

namespace Picums.Data.Tests.CQRS.Queries
{
    public sealed class QueryByIdTests
    {
        private readonly IDatabaseConfiguration configuration;
        private readonly IDataContext context;
        private readonly Guid id;

        public QueryByIdTests()
        {
            this.configuration
                = new InMemoryDbConfiguration(
                    "test",
                    new Dictionary<string, string>
                    {
                        {"SimpleAggregateRoot",  "test" },
                    });
            this.id = Guid.NewGuid();
            var storage = new Dictionary<string, object>
            {
                ["SimpleAggregateRoot"] = new List<SimpleAggregateRoot>
                {
                    new SimpleAggregateRoot(id),
                },
            };
            this.context = new InMemoryDataContext(storage);
        }

        [Fact]
        public async Task QueringForExistingEntity()
        {
            var query = new QueryById<SimpleAggregateRoot>(
                    this.context,
                    LogManager.GetCurrentClassLogger(),
                    this.configuration,
                    this.id);

            var result = await query.Execute();

            result.IsSome.Should().Be(true);
            result.Value.Id.Should().Be(this.id);
        }
    }
}