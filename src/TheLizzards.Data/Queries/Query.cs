using System.Threading.Tasks;
using Serilog;
using TheLizzards.Data.CQRS.Contracts;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.Queries
{
	public abstract class Query<TPayload, TResult> : IAsyncQuery<TResult>
			where TPayload : IAggregateRoot
	{
		protected readonly ILogger logger;
		private readonly IDataContext storageContext;
		private readonly DatabaseParts parts;

		public Query(IDataContext storageContext, ILogger logger, DatabaseParts parts)
		{
			this.storageContext = storageContext;
			this.logger = logger;
			this.parts = parts;
		}

		public void Dispose()
		{
		}

		public abstract Task<TResult> Execute();

		protected IDataReader<TPayload> Read() => this.storageContext.Read<TPayload>(this.parts.Parts);
	}
}