using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;

namespace Picums.Data.CQRS.Queries
{
    public sealed class QueryForAll<TPayload>
        : IWithDatabaseParts<IAsyncQuery<IEnumerable<TPayload>>>
        , IAsyncQuery<IEnumerable<TPayload>>
            where TPayload : IAggregateRoot
    {
        private readonly IDataContext dataContext;
        private readonly ILogger logger;
        private readonly DatabaseParts parts;

        public QueryForAll(IDataContext dataContext, ILogger logger, DatabaseParts parts)
        {
            this.dataContext = dataContext;
            this.logger = logger;
            this.parts = parts;
        }

        public IAsyncQuery<IEnumerable<TPayload>> WithDatabaseParts(DatabaseParts parts)
            => new QueryForAll<TPayload>(
                this.dataContext,
                this.logger,
                parts);

        public Task<IEnumerable<TPayload>> Execute()
            => new QueryForAllBuilder<TPayload>()
                .WithDataContext(this.dataContext)
                .WithLogger(this.logger)
                .WithDatabaseParts(this.parts)
                .Execute();
    }
}