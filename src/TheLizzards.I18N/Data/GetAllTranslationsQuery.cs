using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.CQRS.Queries;

namespace TheLizzards.I18N.Data
{
    public sealed class GetAllTranslationsQuery : QueryProvider<IEnumerable<TranslationItem>>
    {
        public GetAllTranslationsQuery(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts)
            : base(dataContext, loggerFactory, parts)
        {
        }

        public IAsyncQuery<IEnumerable<TranslationItem>> GetQuery()
            => new QueryForAll<TranslationItem>(this.dataContext, this.loggerFactory, this.parts);
    }
}