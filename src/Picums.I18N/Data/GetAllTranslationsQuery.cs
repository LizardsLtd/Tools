using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.CQRS.Queries;

namespace Picums.I18N.Data
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