using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;

namespace Picums.Data.CQRS.Queries
{
    public sealed class QueryForAll<TPayload>
        : IWithConfiguration<IAsyncQuery<IEnumerable<TPayload>>>
        , IAsyncQuery<IEnumerable<TPayload>>
            where TPayload : IAggregateRoot
    {
        private readonly IDataContext dataContext;
        private readonly ILogger logger;
        private readonly IDatabaseConfiguration configuration;

        public QueryForAll(IDataContext dataContext, ILogger logger, IDatabaseConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.logger = logger;
            this.configuration = configuration;
        }

        public IAsyncQuery<IEnumerable<TPayload>> WithConfiguration(IDatabaseConfiguration configuration)
            => new QueryForAll<TPayload>(
                this.dataContext,
                this.logger,
                configuration);

        public async Task<IEnumerable<TPayload>> Execute()
            => (await this.ExecuteQuery()).ToArray();

        private async Task<IQueryable<TPayload>> ExecuteQuery()
            => await new QueryForAllBuilder<TPayload>()
                .WithDataContext(this.dataContext)
                .WithLogger(this.logger)
                .Execute();
    }
}