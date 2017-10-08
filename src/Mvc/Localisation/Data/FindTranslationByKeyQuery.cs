using System;
using System.Threading.Tasks;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.CQRS.Queries;
using Picums.Maybe;

namespace Picums.Mvc.Localisation.DataStorage
{
    public sealed class FindTranslationByKeyQuery : QueryProvider<Maybe<TranslationItem>>, IsQuery
    {
        public FindTranslationByKeyQuery(IDataContext dataContext, ILogger logger, DatabaseParts parts)
            : base(dataContext, logger, parts) { }

        public async Task<Maybe<TranslationItem>> GetByKey(TranslationItem newItem)
        {
            var query
                = new QueryBySpecification<TranslationItem>(
                    this.dataContext
                    , this.logger
                    , this.parts
                    , item
                        => item.TranslationKey.Equals(newItem.TranslationKey, StringComparison.OrdinalIgnoreCase)
                        && item.CultureName.Equals(newItem.CultureName, StringComparison.OrdinalIgnoreCase));

            var translationItems = await query.Execute();

            return translationItems.SingleOrNothing();
        }
    }
}