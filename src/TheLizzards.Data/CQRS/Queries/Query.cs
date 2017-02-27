using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.Contracts;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.DDD;

namespace TheLizzards.Data.CQRS.Queries
{
	public sealed class Query<TPayload, TResult> : IAsyncQuery<TResult>
			where TPayload : IAggregateRoot
	{
		private readonly ILogger logger;
		private readonly IDataContext storageContext;
		private readonly DatabaseParts parts;
		private readonly Func<IDataReader<TPayload>, Task<TResult>> execute;

		public Query(
			IDataContext storageContext
			, ILoggerFactory loggerFactory
			, DatabaseParts parts
			, Func<IDataReader<TPayload>, Task<TResult>> execute)
		{
			this.storageContext = storageContext;
			this.logger = loggerFactory.CreateLogger(GetType());
			this.parts = parts;
			this.execute = execute;
		}

		public void Dispose()
		{
		}

		public Task<TResult> Execute()
			=> this.execute(this.Read());

		private IDataReader<TPayload> Read()
			=> this.storageContext.GetReader<TPayload>(this.parts.Parts);
	}
}