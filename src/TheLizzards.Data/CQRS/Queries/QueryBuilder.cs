using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.DataAccess;

namespace TheLizzards.Data.CQRS.Queries
{
	public abstract class QueryBuilder<TResult> : IQueryBuilder<TResult>
	{
		protected IDataContext dataContext;
		protected ILoggerFactory loggerFactory;
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
		{
			this.loggerFactory = loggerFactory;
			return this;
		}

		protected abstract TResult NextBuildStep();
	}
}