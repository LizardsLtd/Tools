using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.CQRS.Queries
{
	public abstract class QueryByIdWithoutDefault<TPayload> : Query<TPayload, TPayload>
		where TPayload : IAggregateRoot
	{
		private readonly Guid id;

		public QueryByIdWithoutDefault(
			IDataContext storageContext
			, ILoggerFactory loggerfactory
			, DatabaseParts parts, Guid id)
				: base(storageContext, loggerfactory, parts)
		{
			this.id = id;
		}

		public override Task<TPayload> Execute()
			=> this.Read().QueryFor(items => items.Single(x => x.Id == this.id));
	}
}