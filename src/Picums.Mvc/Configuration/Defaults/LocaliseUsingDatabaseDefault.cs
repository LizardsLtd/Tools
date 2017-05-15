using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Picums.Data.CQRS.DataAccess;
using Picums.Localisation.Data;
using Picums.Localisation.Data.Services;
using Microsoft.Extensions.Logging;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class LocaliseUsingDatabaseDefault : IDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            host.Services.Add(services => services
                    //.AddTransient<GetAllTranslationsQuery>()
                    .AddTransient<AddNewTranslationCommandHandler>()
                    .AddTransient(serviceProvider => GetTranslationSetProvider(serviceProvider, GetParts(arguments))));

            host.Apply<CQRSDefaults>("Picums.Localisation");
        }

        private ITranslationSetProvider GetTranslationSetProvider(IServiceProvider serviceProvider, DatabaseParts parts)
            => new DataTranslationProvider(GetTranslationQuery(serviceProvider), parts);

        private GetAllTranslationsQuery GetTranslationQuery(IServiceProvider serviceProvider)
            => new GetAllTranslationsQuery(serviceProvider.GetService<IDataContext>(), serviceProvider.GetService<ILoggerFactory>());

        private DatabaseParts GetParts(IEnumerable<object> arguments) => arguments.ElementAt(0) as DatabaseParts;
    }
}