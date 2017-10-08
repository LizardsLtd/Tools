using NLog;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Data.CQRS.Queries
{
    public abstract class QueryProvider<TResult>
    {
        protected readonly IDataContext dataContext;
        protected readonly ILogger logger;
        protected readonly DatabaseParts parts;

        public QueryProvider(IDataContext dataContext, ILogger logger, DatabaseParts parts)
        {
            this.dataContext = dataContext;
            this.logger = logger;
            this.parts = parts;
        }
    }
}