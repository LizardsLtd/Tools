using NLog;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Data.CQRS.Queries
{
    public abstract class QueryBuilder<TResult> : IQueryBuilder<TResult>
    {
        protected IDataContext dataContext;
        protected ILogger logger;

        public IWithLogger<TResult> WithDataContext(IDataContext dataContext)
        {
            this.dataContext = dataContext;
            return this;
        }

        public TResult WithLogger(ILogger logger)
        {
            this.logger = logger;
            return this.NextBuildStep();
        }

        protected abstract TResult NextBuildStep();
    }
}