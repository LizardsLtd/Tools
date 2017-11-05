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
        private readonly IDatabaseConfiguration configuration;

        public GetAllTranslationsQuery(IDataContext dataContext, ILogger logger, IDatabaseConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<TranslationItem>> Execute()
            => await new QueryForAllBuilder<TranslationItem>()
                .WithDataContext(this.dataContext)
                .WithLogger(this.logger)
                .Execute();
    }
}