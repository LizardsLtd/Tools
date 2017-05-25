using Microsoft.Extensions.Logging;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Data.CQRS.Queries
{
    public abstract class QueryBuilder<TResult> : IQueryBuilder<TResult>
    {
        protected IDataContext dataContext;
        protected ILogger logger;
        protected DatabaseParts parts;

        public TResult WithDatabaseParts(DatabaseParts parts)
        {
            this.parts = parts;
            return this.NextBuildStep();
        }

        public IWithLogger<IWithDatabaseParts<TResult>> WithDataContext(IDataContext dataContext)
        {
            this.dataContext = dataContext;
            return this;
        }

        public IWithDatabaseParts<TResult> WithLogger(ILoggerFactory loggerFactory)
            => WithLogger(loggerFactory.CreateLogger("query"));

        public IWithDatabaseParts<TResult> WithLogger(ILogger logger)
        {
            this.logger = logger;
            return this;
        }

        protected abstract TResult NextBuildStep();
    }
}