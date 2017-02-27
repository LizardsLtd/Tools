using System;
using System.Linq;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;

namespace TheLizzards.Data.CQRS.Queries
{
	public abstract class QueryByIdWithoutDefault<TPayload>
		: QueryBuilder<IWithId<IAsyncQuery<TPayload>>>
		, IQueryBuilder<IWithId<IAsyncQuery<TPayload>>>
		, IWithId<IAsyncQuery<TPayload>>
			where TPayload : IAggregateRoot
	{
		public IAsyncQuery<TPayload> WithId(Guid id)
			=> new Query<TPayload, TPayload>(
				this.dataContext
				, this.loggerFactory
				, this.parts
				, reader => this.Execute(reader, id));

		public Task<TPayload> Execute(IDataReader<TPayload> reader, Guid id)
			=> reader.QueryFor(items => items.Single(x => x.Id == id));

		protected override IWithId<IAsyncQuery<TPayload>> NextBuildStep() => this;
	}
}