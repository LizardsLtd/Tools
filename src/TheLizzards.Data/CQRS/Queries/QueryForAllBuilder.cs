using System.Collections.Generic;
using TheLizzards.Data.Domain;

namespace TheLizzards.Data.CQRS.Queries
{
	internal sealed class QueryForAllBuilder<TPayload>
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