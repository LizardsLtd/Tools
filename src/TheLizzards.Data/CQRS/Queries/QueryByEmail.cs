using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;
using TheLizzards.Data.Types;
using TheLizzards.Maybe;

namespace TheLizzards.Data.CQRS.Queries
{
	public abstract class QueryByEmail<TPayload> : IAsyncQuery<Maybe<TPayload>>
		where TPayload : IAggregateRoot, IWithEmailIndex
	{
		private readonly IDataContext dataContext;
		private readonly ILoggerFactory loggerFactory;
		private readonly DatabaseParts parts;
		private readonly Email email;

		protected QueryByEmail(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts, Email email)
		{
			this.dataContext = dataContext;
			this.loggerFactory = loggerFactory;
			this.parts = parts;
			this.email = email;
		}

		public Task<Maybe<TPayload>> Execute()
			=> new QueryByEmailBuilder<TPayload>()
				.WithDataContext(this.dataContext)
				.WithLogger(this.loggerFactory)
				.WithDatabaseParts(this.parts)
				.WithEmail(this.email)
				.Execute();
	}
}