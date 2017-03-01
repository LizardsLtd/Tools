using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;
using TheLizzards.Maybe;

namespace TheLizzards.Data.CQRS.Queries
{
	public abstract class QueryByName<TPayload> : IAsyncQuery<Maybe<TPayload>>
		where TPayload : IAggregateRoot
	{
		private readonly IDataContext dataContext;
		private readonly ILoggerFactory loggerFactory;
		private readonly DatabaseParts parts;
		private readonly string name;

		protected QueryByName(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts, string name)
		{
			this.dataContext = dataContext;
			this.loggerFactory = loggerFactory;
			this.parts = parts;
			this.name = name;
		}

		public Task<Maybe<TPayload>> Execute()
			=> new QueryByNameBuilder<TPayload>()
				.WithDataContext(this.dataContext)
				.WithLogger(this.loggerFactory)
				.WithDatabaseParts(this.parts)
				.WithName(this.name)
				.Execute();
	}
}