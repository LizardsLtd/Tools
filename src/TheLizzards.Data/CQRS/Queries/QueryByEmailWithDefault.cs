using System.Linq;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;
using TheLizzards.Data.Types;
using TheLizzards.Maybe;

namespace TheLizzards.Data.CQRS.Queries
{
	public sealed class QueryByEmailWithDefault<TPayload>
		: QueryBuilder<IWithEmail<IAsyncQuery<Maybe<TPayload>>>>
		, IQueryBuilder<IWithEmail<IAsyncQuery<Maybe<TPayload>>>>
		, IWithEmail<IAsyncQuery<Maybe<TPayload>>>
			where TPayload : IAggregateRoot, IWithEmailIndex
	{
		public IAsyncQuery<Maybe<TPayload>> WithEmail(Email email)
			=> new Query<TPayload, Maybe<TPayload>>(
				this.dataContext
				, this.logger
				, this.parts
				, reader => this.Execute(reader, email));

		public Task<Maybe<TPayload>> Execute(IDataReader<TPayload> reader, Email email)
			=> reader.QueryFor(items => (Maybe<TPayload>)items.SingleOrDefault(x => x.Email == email));

		protected override IWithEmail<IAsyncQuery<Maybe<TPayload>>> NextBuildStep() => this;
	}
}