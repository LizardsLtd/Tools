﻿using System;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD.Contracts;
using TheLizzards.Maybe;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Data.Queries
{
	public abstract class QueryByIdWithDefault<TPayload> : Query<TPayload, Maybe<TPayload>>
		where TPayload : IAggregateRoot
	{
		private readonly Guid id;

		public QueryByIdWithDefault(IDataContext storageContext, ILoggerFactory loggerfactory, DatabaseParts parts, Guid id)
			: base(storageContext, loggerfactory, parts)
		{
			this.id = id;
		}

		public override Task<Maybe<TPayload>> Execute() => this.Read().SingleOrDefault(x => x.Id == this.id);
	}
}