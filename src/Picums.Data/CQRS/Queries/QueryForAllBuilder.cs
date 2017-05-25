using System.Collections.Generic;
using Picums.Data.Domain;

namespace Picums.Data.CQRS.Queries
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