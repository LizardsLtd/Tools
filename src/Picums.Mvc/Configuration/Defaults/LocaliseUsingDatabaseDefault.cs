using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Picums.Data.CQRS.DataAccess;
using Picums.Localisation.Data;
using Picums.Localisation.Data.Services;
using Picums.Mvc.Middleware;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class LocaliseUsingDatabaseDefault : IDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            host.Services.Add(x => x.AddTransient<GetAllTranslationsQuery>());
            host.Services.Add(x => x.AddTransient<ITranslationSetProvider, DataTranslationProvider>());
            host.Services.Add(x => x.AddSingleton(arguments.ElementAt(0) as DatabaseParts));

            host.Apply<CQRSDefaults>("Picums.Localisation");
            host.Apply<MiddlewareDefault<CultureCookieSetterMiddleware>>();
        }
    }
}