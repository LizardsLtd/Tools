using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Picums.Data.CQRS.DataAccess;
using Picums.Localisation.Data;
using Picums.Localisation.Data.Services;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class LocaliseUsingDatabaseDefault : IDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            host.Services.Add(services => services
                    .AddTransient<GetAllTranslationsQuery>()
                    .AddTransient<AddNewTranslationCommandHandler>()
                    .AddTransient<ITranslationSetProvider, DataTranslationProvider>());

            host.Apply<CQRSDefaults>("Picums.Localisation");
        }
    }
}