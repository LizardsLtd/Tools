using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.DataAccess;

namespace TheLizzards.Data.CQRS.Queries
{
    public abstract class QueryProvider<TResult>
    {
        protected readonly IDataContext dataContext;
        protected readonly ILoggerFactory loggerFactory;
        protected readonly DatabaseParts parts;

        public QueryProvider(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts)
        {
            this.dataContext = dataContext;
            this.loggerFactory = loggerFactory;
            this.parts = parts;
        }
    }
}