using System.Collections.Generic;
using System.Linq;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Mvc.Localisation.DataStorage
{
    public sealed class DataTranslationProvider : ITranslationSetProvider
    {
        private readonly IAsyncQuery<IEnumerable<TranslationItem>> query;
        private readonly ILogger logger;

        public DataTranslationProvider(GetAllTranslationsQuery query, DatabaseParts parts, ILogger logger)
        {
            this.query = query.GetQuery(parts);
            this.logger = logger;
        }

        public TranslationSet GetTranslationSet()
        {
            var queryResults = query.Execute().Result.ToArray();

            this.logger.Debug($"ITranslationSetProvider: Query loaded with {queryResults.Length}");

            return new TranslationSet(queryResults);
        }
    }
}