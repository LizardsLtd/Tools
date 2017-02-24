using System;
using System.Threading.Tasks;
using Serilog;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD.Contracts;
using TheLizzards.Maybe;

namespace TheLizzards.Data.Queries
{
	public abstract class QueryByIdWithDefault<TPayload> : Query<TPayload, Maybe<TPayload>>
		where TPayload : IAggregateRoot
	{
		private readonly Guid id;

		public QueryByIdWithDefault(IDataContext storageContext, ILogger logger, DatabaseParts parts, Guid id)
			: base(storageContext, logger, parts)
		{
			this.id = id;
		}

		public override Task<Maybe<TPayload>> Execute() => this.Read().SingleOrDefault(x => x.Id == this.id);
	}
}