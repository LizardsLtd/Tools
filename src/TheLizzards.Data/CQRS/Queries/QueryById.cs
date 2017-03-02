using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;
using TheLizzards.Maybe;

namespace TheLizzards.Data.CQRS.Queries
{
	public abstract class QueryById<TPayload> 
		: IWithDatabaseParts<IWithId<IAsyncQuery<Maybe<TPayload>>>>
		, IWithId<IAsyncQuery<Maybe<TPayload>>>
		, IAsyncQuery<Maybe<TPayload>>
			where TPayload : IAggregateRoot
	{
		private readonly IDataContext dataContext;
		private readonly ILoggerFactory loggerFactory;
		private readonly DatabaseParts parts;
		private readonly Guid id;

		protected QueryById(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts, Guid id)
		{
			this.dataContext = dataContext;
			this.loggerFactory = loggerFactory;
			this.parts = parts;
			this.id = id;
		}
		
		public IWithId<IAsyncQuery<Maybe<TPayload>>> WithDatabaseParts(DatabaseParts parts)
			=> new QueryById<TPayload>(
				this.dataContext
				, this.loggerFactory
				, parts
				, Guid.Empty);
				
		public IAsyncQuery<Maybe<TPayload>> WithId(Guid id)
			=> new QueryById<TPayload>(
				this.dataContext
				, this.loggerFactory
				, this.parts
				, id);	

		public Task<Maybe<TPayload>> Execute()
			=> new QueryByIdBuilder<TPayload>()
				.WithDataContext(this.dataContext)
				.WithLogger(this.loggerFactory)
				.WithDatabaseParts(this.parts)
				.WithId(this.id)
				.Execute();
	}
}
