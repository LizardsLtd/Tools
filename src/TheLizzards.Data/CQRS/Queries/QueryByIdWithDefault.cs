using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD;
using TheLizzards.Maybe;

namespace TheLizzards.Data.CQRS.Queries
{
	public abstract class QueryByIdWithDefault<TPayload> : Query<TPayload, Maybe<TPayload>>
		where TPayload : IAggregateRoot
	{
		private readonly Guid id;

		public QueryByIdWithDefault(
			IDataContext storageContext
			, ILoggerFactory loggerfactory
			, DatabaseParts parts, Guid id)
				: base(storageContext, loggerfactory, parts)
		{
			this.id = id;
		}

		public override Task<Maybe<TPayload>> Execute()
			=> this.Read().QueryFor(items => (Maybe<TPayload>)items.SingleOrDefault(x => x.Id == this.id));
	}
}