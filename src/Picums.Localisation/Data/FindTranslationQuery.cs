using Microsoft.Extensions.Logging;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.CQRS.Queries;
using Picums.Maybe;
using System.Globalization;
using System.Threading.Tasks;

namespace Picums.Localisation.Data
{
    public sealed class FindTranslationQuery : QueryProvider<Maybe<TranslationItem>>, IsQuery
    {
        public FindTranslationQuery(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts)
            : base(dataContext, loggerFactory, parts) { }

        public async Task<Maybe<TranslationItem>> Execute(CultureInfo culture, string key)
            => (await new QueryBySpecificationBuilder<TranslationItem>()
                    .WithDataContext(this.dataContext)
                    .WithLogger(this.loggerFactory)
                    .WithDatabaseParts(this.parts)
                    .WithSpecification(item => item.Compare(culture, key))
                    .Execute())
                .FirstOrNothing();
    }
}