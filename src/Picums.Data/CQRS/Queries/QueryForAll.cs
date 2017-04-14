using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
		private readonly ILoggerFactory loggerFactory;
		private readonly DatabaseParts parts;

		public QueryForAll(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts)
		{
			this.dataContext = dataContext;
			this.loggerFactory = loggerFactory;
			this.parts = parts;
		}

		public IAsyncQuery<IEnumerable<TPayload>> WithDatabaseParts(DatabaseParts parts)
			=> new QueryForAll<TPayload>(
				this.dataContext
				, this.loggerFactory
				, parts);

		public Task<IEnumerable<TPayload>> Execute()
			=> new QueryForAllBuilder<TPayload>()
				.WithDataContext(this.dataContext)
				.WithLogger(this.loggerFactory)
				.WithDatabaseParts(this.parts)
				.Execute();
	}
}