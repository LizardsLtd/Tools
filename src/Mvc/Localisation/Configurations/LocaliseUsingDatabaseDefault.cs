using System;
using System.Collections.Generic;
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
            services
                .AddTransient(serviceProvider => this.GetNewTranslationCommandHandler(serviceProvider))
                .AddTransient(serviceProvider => this.GetTranslationSetProvider(serviceProvider));
        }

        private ICommandHandler GetNewTranslationCommandHandler(IServiceProvider serviceProvider)
            => new AddNewTranslationCommandHandler(
                serviceProvider.GetService<IDataContext>(),
                serviceProvider.GetService<ILogger>());

        private ITranslationSetProvider GetTranslationSetProvider(IServiceProvider serviceProvider)
            => new DataTranslationProvider(
                this.GetTranslationQuery(serviceProvider),
                this.GetLogger(serviceProvider));

        private GetAllTranslationsQuery GetTranslationQuery(IServiceProvider serviceProvider)
            => new GetAllTranslationsQuery(
                serviceProvider.GetService<IDataContext>(),
                this.GetLogger(serviceProvider),
                serviceProvider.GetService<IDatabaseConfiguration>());

        private ILogger GetLogger(IServiceProvider serviceProvider) => serviceProvider.GetRequiredService<ILogger>();
    }
}