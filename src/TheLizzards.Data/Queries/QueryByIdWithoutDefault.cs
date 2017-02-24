using System;
using System.Threading.Tasks;
using Serilog;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.Queries
{
	public abstract class QueryByIdWithoutDefault<TPayload> : Query<TPayload, TPayload>
		where TPayload : IAggregateRoot
	{
		private readonly Guid id;

		public QueryByIdWithoutDefault(IDataContext storageContext, ILogger logger, DatabaseParts parts, Guid id)
			: base(storageContext, logger, parts)
		{
			this.id = id;
		}

		public override Task<TPayload> Execute() => this.Read().Single(x => x.Id == this.id);
	}
}