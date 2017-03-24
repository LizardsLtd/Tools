using System;
using System.Linq;
using System.Threading.Tasks;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;
using Picums.Maybe;

namespace Picums.Data.CQRS.Queries
{
	public sealed class QueryByIdBuilder<TPayload>
		: QueryBuilder<IWithId<IAsyncQuery<Maybe<TPayload>>>>
		, IWithId<IAsyncQuery<Maybe<TPayload>>>
			where TPayload : IAggregateRoot
	{
		public IAsyncQuery<Maybe<TPayload>> WithId(Guid id)
			=> new Query<TPayload, Maybe<TPayload>>(
				this.dataContext
				, this.logger
				, this.parts
				, reader => this.Execute(reader, id));

		protected override IWithId<IAsyncQuery<Maybe<TPayload>>> NextBuildStep() => this;

		private Task<Maybe<TPayload>> Execute(IDataReader<TPayload> reader, Guid id)
					=> reader.QueryFor(items => (Maybe<TPayload>)items.SingleOrDefault(x => x.Id == id));
	}
}