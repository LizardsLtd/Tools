using System.Linq;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;
using TheLizzards.Maybe;

namespace TheLizzards.Data.CQRS.Queries
{
	public sealed class QueryByNameWithDefault<TPayload>
		: QueryBuilder<IWithName<IAsyncQuery<Maybe<TPayload>>>>
		, IQueryBuilder<IWithName<IAsyncQuery<Maybe<TPayload>>>>
		, IWithName<IAsyncQuery<Maybe<TPayload>>>
			where TPayload : IAggregateRoot, IWithNameIndex
	{
		public IAsyncQuery<Maybe<TPayload>> WithName(string name)
			=> new Query<TPayload, Maybe<TPayload>>(
				this.dataContext
				, this.logger
				, this.parts
				, reader => this.Execute(reader, name));

		public Task<Maybe<TPayload>> Execute(IDataReader<TPayload> reader, string name)
			=> reader.QueryFor(items => (Maybe<TPayload>)items.SingleOrDefault(x => x.Name == name));

		protected override IWithName<IAsyncQuery<Maybe<TPayload>>> NextBuildStep() => this;
	}
}