using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
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

        public async Task<IEnumerable<TranslationItem>> Execute()
            => await new QueryForAllBuilder<TranslationItem>()
                .WithDataContext(this.dataContext)
                .WithLogger(this.logger)
                .Execute();
    }
}