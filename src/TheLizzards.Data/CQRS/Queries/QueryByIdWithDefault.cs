using System;
using System.Linq;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;
using TheLizzards.Maybe;

namespace TheLizzards.Data.CQRS.Queries
{
	public abstract class QueryByIdWithDefault<TPayload>
		: QueryBuilder<IWithId<IAsyncQuery<Maybe<TPayload>>>>
		, IQueryBuilder<IWithId<IAsyncQuery<Maybe<TPayload>>>>
		, IWithId<IAsyncQuery<Maybe<TPayload>>>
			where TPayload : IAggregateRoot
	{
		public IAsyncQuery<Maybe<TPayload>> WithId(Guid id)
			=> new Query<TPayload, Maybe<TPayload>>(
				this.dataContext
				, this.loggerFactory
				, this.parts
				, reader => this.Execute(reader, id));

		public Task<Maybe<TPayload>> Execute(IDataReader<TPayload> reader, Guid id)
			=> reader.QueryFor(items => (Maybe<TPayload>)items.SingleOrDefault(x => x.Id == id));

		protected override IWithId<IAsyncQuery<Maybe<TPayload>>> NextBuildStep() => this;
	}
}