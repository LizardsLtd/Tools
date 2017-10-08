using System.Collections.Generic;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.CQRS.Queries;

namespace Picums.Mvc.Localisation.DataStorage
{
    public sealed class GetAllTranslationsQuery
    {
        private readonly IDataContext dataContext;
        private readonly ILogger logger;

        public GetAllTranslationsQuery(IDataContext dataContext, ILogger logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public IAsyncQuery<IEnumerable<TranslationItem>> GetQuery(DatabaseParts parts)
            => new QueryForAll<TranslationItem>(this.dataContext, this.logger, parts);
    }
}