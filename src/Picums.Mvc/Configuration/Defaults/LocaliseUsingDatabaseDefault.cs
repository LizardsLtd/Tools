using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Picums.Data.CQRS.DataAccess;
using Picums.Localisation.Data;
using Picums.Localisation.Data.Services;
using Microsoft.Extensions.Logging;
using Picums.Data.CQRS;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class LocaliseUsingDatabaseDefault : BasicDefault
    {
        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
        {
            var databasePartsForTranslation = GetParts(arguments);
            services
                .AddTransient(serviceProvider => GetNewTranslationCommandHandler(serviceProvider, databasePartsForTranslation))
                .AddTransient(serviceProvider => GetTranslationSetProvider(serviceProvider, databasePartsForTranslation));
        }

        private ICommandHandler GetNewTranslationCommandHandler(IServiceProvider serviceProvider, DatabaseParts databasePartsForTranslation)
            => new AddNewTranslationCommandHandler(serviceProvider.GetService<IDataContext>(), databasePartsForTranslation);

        private ITranslationSetProvider GetTranslationSetProvider(IServiceProvider serviceProvider, DatabaseParts parts)
            => new DataTranslationProvider(GetTranslationQuery(serviceProvider), parts, this.GetLoggerFactory(serviceProvider));

        private GetAllTranslationsQuery GetTranslationQuery(IServiceProvider serviceProvider)
            => new GetAllTranslationsQuery(serviceProvider.GetService<IDataContext>(), serviceProvider.GetService<ILoggerFactory>());

        private DatabaseParts GetParts(IEnumerable<object> arguments) => arguments.ElementAt(0) as DatabaseParts;

        private ILoggerFactory GetLoggerFactory(IServiceProvider serviceProvider) => serviceProvider.GetRequiredService<ILoggerFactory>();
    }
}