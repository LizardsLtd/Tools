using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;

namespace TheLizzards.Data.CQRS.Queries
{
	public sealed class QueryBySpecification<TPayload> : IAsyncQuery<IQueryable<TPayload>>
		where TPayload : IAggregateRoot
	{
		private readonly IDataContext dataContext;
		private readonly ILoggerFactory loggerFactory;
		private readonly DatabaseParts parts;
		private readonly Expression<Func<TPayload, bool>> specification;

		public QueryBySpecification(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts, Expression<Func<TPayload, bool>> specification)
		{
			this.dataContext = dataContext;
			this.loggerFactory = loggerFactory;
			this.parts = parts;
			this.specification = specification;
		}

		public Task<IQueryable<TPayload>> Execute()
			=> new QueryBySpecificationBuilder<TPayload>()
				.WithDataContext(this.dataContext)
				.WithLogger(this.loggerFactory)
				.WithDatabaseParts(this.parts)
				.WithSpecification(specification)
				.Execute();
	}
}