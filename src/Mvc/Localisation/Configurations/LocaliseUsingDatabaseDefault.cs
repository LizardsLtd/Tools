using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Mvc.Configuration.Defaults;
using Picums.Mvc.Localisation.DataStorage;

namespace Picums.Mvc.Localisation.Configuration.Defaults
{
    public sealed class LocaliseUsingDatabaseDefault : BasicDefault
    {
        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
        {
            var databasePartsForTranslation = this.GetParts(arguments);
            services
                .AddTransient(serviceProvider => this.GetNewTranslationCommandHandler(serviceProvider, databasePartsForTranslation))
                .AddTransient(serviceProvider => this.GetTranslationSetProvider(serviceProvider, databasePartsForTranslation));
        }

        private ICommandHandler GetNewTranslationCommandHandler(IServiceProvider serviceProvider, DatabaseParts databasePartsForTranslation)
            => new AddNewTranslationCommandHandler(
                serviceProvider.GetService<IDataContext>(),
                serviceProvider.GetService<ILogger>(),
                databasePartsForTranslation);

        private ITranslationSetProvider GetTranslationSetProvider(IServiceProvider serviceProvider, DatabaseParts parts)
            => new DataTranslationProvider(this.GetTranslationQuery(serviceProvider), parts, this.GetLoggerFactory(serviceProvider));

        private GetAllTranslationsQuery GetTranslationQuery(IServiceProvider serviceProvider)
            => new GetAllTranslationsQuery(serviceProvider.GetService<IDataContext>(), serviceProvider.GetService<ILogger>());

        private DatabaseParts GetParts(IEnumerable<object> arguments) => arguments.ElementAt(0) as DatabaseParts;

        private ILogger GetLoggerFactory(IServiceProvider serviceProvider) => serviceProvider.GetRequiredService<ILogger>();
    }
}