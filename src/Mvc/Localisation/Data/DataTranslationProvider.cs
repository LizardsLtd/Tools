using System.Linq;
using NLog;

namespace Picums.Mvc.Localisation.DataStorage
{
    public sealed class DataTranslationProvider : ITranslationSetProvider
    {
        private readonly GetAllTranslationsQuery query;
        private readonly ILogger logger;

        public DataTranslationProvider(GetAllTranslationsQuery query, ILogger logger)
        {
            this.query = query;
            this.logger = logger;
        }

        public TranslationSet GetTranslationSet()
        {
            var queryResults = this.query
                .Execute()
                .Result
                .ToArray();

            this.logger.Debug($"ITranslationSetProvider: Query loaded with {queryResults.Length}");

            return new TranslationSet(queryResults);
        }
    }
}