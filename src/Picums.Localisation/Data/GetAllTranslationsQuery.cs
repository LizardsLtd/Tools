using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.CQRS.Queries;

namespace Picums.Localisation.Data
{
    public sealed class GetAllTranslationsQuery
    {
        private readonly IDataContext dataContext;
        private readonly ILoggerFactory loggerFactory;

        public GetAllTranslationsQuery(IDataContext dataContext, ILoggerFactory loggerFactory)
        {
            this.dataContext = dataContext;
            this.loggerFactory = loggerFactory;
        }

        public IAsyncQuery<IEnumerable<TranslationItem>> GetQuery(DatabaseParts parts)
            => new QueryForAll<TranslationItem>(this.dataContext, this.loggerFactory, parts);
    }
}