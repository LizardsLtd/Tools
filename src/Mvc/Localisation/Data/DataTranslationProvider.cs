using System.Linq;
using NLog;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Mvc.Localisation.DataStorage
{
    public sealed class DataTranslationProvider : ITranslationSetProvider
    {
        private readonly GetAllTranslationsQuery query;
        private readonly DatabaseParts parts;
        private readonly ILogger logger;

        public DataTranslationProvider(GetAllTranslationsQuery query, DatabaseParts parts, ILogger logger)
        {
            this.query = query;
            this.parts = parts;
            this.logger = logger;
        }

        public TranslationSet GetTranslationSet()
        {
            var queryResults = this.query
                .Execute(this.parts)
                .Result
                .ToArray();

            this.logger.Debug($"ITranslationSetProvider: Query loaded with {queryResults.Length}");

            return new TranslationSet(queryResults);
        }
    }
}