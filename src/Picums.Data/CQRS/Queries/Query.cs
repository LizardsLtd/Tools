using System;
using System.Threading.Tasks;
using NLog;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;

namespace Picums.Data.CQRS.Queries
{
    public sealed class Query<TPayload, TResult> : IAsyncQuery<TResult>
            where TPayload : IAggregateRoot
    {
        private readonly IDataContext storageContext;
        private readonly ILogger logger;
        private readonly DatabaseParts parts;
        private readonly Func<IDataReader<TPayload>, Task<TResult>> execute;

        public Query(
            IDataContext storageContext,
            ILogger logger,
            DatabaseParts parts,
            Func<IDataReader<TPayload>, Task<TResult>> execute)
        {
            this.storageContext = storageContext;
            this.logger = logger;
            this.parts = parts;
            this.execute = execute;
        }

        public Task<TResult> Execute()
            => this.execute(this.Read());

        private IDataReader<TPayload> Read()
            => this.storageContext.GetReader<TPayload>(this.parts);
    }
}