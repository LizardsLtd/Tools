using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.DDD;

namespace TheLizzards.Data.CQRS.Queries
{
	public sealed class QueryBySpecificationBuilder<TPayload>
		: QueryBuilder<IWithSpecification<IAsyncQuery<IQueryable<TPayload>>, TPayload>>
		, IWithSpecification<IAsyncQuery<IQueryable<TPayload>>, TPayload>
			where TPayload : IAggregateRoot
	{
		public IAsyncQuery<IQueryable<TPayload>> WithSpecification(Expression<Func<TPayload, bool>> specification)
			=> new Query<TPayload, IQueryable<TPayload>>(
				this.dataContext
				, this.logger
				, this.parts
				, reader => this.Execute(reader, specification));

		protected override IWithSpecification<IAsyncQuery<IQueryable<TPayload>>, TPayload> NextBuildStep() => this;

		private Task<IQueryable<TPayload>> Execute(IDataReader<TPayload> reader, Expression<Func<TPayload, bool>> specification)
			=> reader.Where(specification);
	}
}