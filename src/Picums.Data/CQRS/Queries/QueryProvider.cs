using NLog;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Data.CQRS.Queries
{
    public abstract class QueryProvider<TResult>
    {
        public QueryProvider(IDataContext dataContext, ILogger logger)
        {
            this.DataContext = dataContext;
            this.Logger = logger;
        }

        public ILogger Logger { get; }

        protected IDataContext DataContext { get; }
    }
}