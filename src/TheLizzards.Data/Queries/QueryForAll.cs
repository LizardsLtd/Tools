using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.Queries
{
	public abstract class QueryForAll<TPayload> : Query<TPayload, IEnumerable<TPayload>>
		where TPayload : IAggregateRoot
	{
		private readonly Guid id;

		public QueryForAll(
			IDataContext storageContext
			, ILoggerFactory loggerfactory
			, DatabaseParts parts, Guid id)
				: base(storageContext, loggerfactory, parts)
		{
			this.id = id;
		}

		public override Task<IEnumerable<TPayload>> Execute()
			=> this.Read().All();
	}
}