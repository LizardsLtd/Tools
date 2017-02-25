using System;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD.Contracts;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Data.Queries
{
	public abstract class QueryByIdWithoutDefault<TPayload> : Query<TPayload, TPayload>
		where TPayload : IAggregateRoot
	{
		private readonly Guid id;

		public QueryByIdWithoutDefault(IDataContext storageContext, ILoggerFactory loggerfactory, DatabaseParts parts, Guid id)
			: base(storageContext, loggerfactory, parts)
		{
			this.id = id;
		}

		public override Task<TPayload> Execute() => this.Read().Single(x => x.Id == this.id);
	}
}