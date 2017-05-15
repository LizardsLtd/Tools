using System.Collections.Generic;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Localisation.Data.Services
{
    public sealed class DataTranslationProvider : ITranslationSetProvider
    {
        private readonly IAsyncQuery<IEnumerable<TranslationItem>> query;

        public DataTranslationProvider(GetAllTranslationsQuery query, DatabaseParts parts )
        {
            this.query = query.GetQuery(parts);
        }

        public TranslationSet GetTranslationSet()
            => new TranslationSet(query.Execute().Result);
    }
}