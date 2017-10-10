using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<TranslationItem>> Execute(DatabaseParts parts)
            => await this.GetQuery(parts).Execute();

        private IAsyncQuery<IEnumerable<TranslationItem>> GetQuery(DatabaseParts parts)
            => new QueryForAll<TranslationItem>(this.dataContext, this.logger, parts);
    }
}