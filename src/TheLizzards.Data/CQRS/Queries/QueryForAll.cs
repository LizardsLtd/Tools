using System.Collections.Generic;
using TheLizzards.Data.DDD;

namespace TheLizzards.Data.CQRS.Queries
{
	public abstract class QueryForAll<TPayload>
		: QueryBuilder<IAsyncQuery<IEnumerable<TPayload>>>
		, IQueryBuilder<IAsyncQuery<IEnumerable<TPayload>>>
			where TPayload : IAggregateRoot
	{
		protected override IAsyncQuery<IEnumerable<TPayload>> NextBuildStep()
			=> new Query<TPayload, IEnumerable<TPayload>>(
				this.dataContext
				, this.logger
				, this.parts
				, x => x.All());
	}
}