using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NLog;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;

namespace Picums.Data.CQRS.Queries
{
    public sealed class QueryBySpecification<TPayload> : IAsyncQuery<IEnumerable<TPayload>>
        where TPayload : IAggregateRoot
    {
        private readonly IDataContext dataContext;
        private readonly ILogger logger;
        private readonly DatabaseParts parts;
        private readonly Expression<Func<TPayload, bool>> specification;

        public QueryBySpecification(
            IDataContext dataContext,
            ILogger logger,
            DatabaseParts parts,
            Expression<Func<TPayload, bool>> specification)
        {
            this.dataContext = dataContext;
            this.logger = logger;
            this.parts = parts;
            this.specification = specification;
        }

        public Task<IEnumerable<TPayload>> Execute()
            => new QueryBySpecificationBuilder<TPayload>()
                .WithDataContext(this.dataContext)
                .WithLogger(this.logger)
                .WithDatabaseParts(this.parts)
                .WithSpecification(this.specification)
                .Execute();
    }
}