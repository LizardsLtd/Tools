using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Localisation.Data.Services
{
    public sealed class DataTranslationProvider : ITranslationSetProvider
    {
        private readonly IAsyncQuery<IEnumerable<TranslationItem>> query;
        private readonly ILogger<ITranslationSetProvider> logger;

        public DataTranslationProvider(GetAllTranslationsQuery query, DatabaseParts parts, ILoggerFactory loggerFactroy)
        {
            this.query = query.GetQuery(parts);
            this.logger = loggerFactroy.CreateLogger<ITranslationSetProvider>();
        }

        public TranslationSet GetTranslationSet()
        {
            var queryResults = query.Execute().Result.ToArray();

            this.logger.LogDebug($"ITranslationSetProvider: Query loaded with {queryResults.Length}");

            return new TranslationSet(queryResults);
        }
    }
}