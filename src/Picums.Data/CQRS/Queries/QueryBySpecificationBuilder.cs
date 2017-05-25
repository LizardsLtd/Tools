using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;

namespace Picums.Data.CQRS.Queries
{
    public sealed class QueryBySpecificationBuilder<TPayload>
        : QueryBuilder<IWithSpecification<IAsyncQuery<IEnumerable<TPayload>>, TPayload>>
        , IWithSpecification<IAsyncQuery<IEnumerable<TPayload>>, TPayload>
            where TPayload : IAggregateRoot
    {
        public IAsyncQuery<IEnumerable<TPayload>> WithSpecification(Expression<Func<TPayload, bool>> specification)
            => new Query<TPayload, IEnumerable<TPayload>>(
                this.dataContext
                , this.logger
                , this.parts
                , reader => this.Execute(reader, specification));

        protected override IWithSpecification<IAsyncQuery<IEnumerable<TPayload>>, TPayload> NextBuildStep() => this;

        private async Task<IEnumerable<TPayload>> Execute(IDataReader<TPayload> reader, Expression<Func<TPayload, bool>> specification)
            => (await reader.Where(specification)).ToArray();
    }
}