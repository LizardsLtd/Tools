using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.CQRS.Queries;
using Picums.Maybe;

namespace Picums.Localisation.Data
{
    public sealed class FindTranslationByKeyQuery : QueryProvider<Maybe<TranslationItem>>, IsQuery
    {
        public FindTranslationByKeyQuery(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts)
            : base(dataContext, loggerFactory, parts) { }

        public async Task<Maybe<TranslationItem>> GetByKey(TranslationItem newItem)
        {
            var query
                = new QueryBySpecification<TranslationItem>(
                    this.dataContext
                    , this.loggerFactory
                    , this.parts
                    , item
                        => item.TranslationKey.Equals(newItem.TranslationKey, StringComparison.OrdinalIgnoreCase)
                        && item.CultureName.Equals(newItem.CultureName, StringComparison.OrdinalIgnoreCase));

            var translationItems = await query.Execute();

            return translationItems.SingleOrNothing();
        }
    }
}