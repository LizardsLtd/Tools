using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Picums.Data.CQRS;
using Picums.Mvc.Configuration.Defaults;
using Picums.Mvc.Localisation.DataStorage;

namespace Picums.Mvc.Localisation.Configuration.Defaults
{
    public sealed class LocaliseUsingDatabaseDefault : BasicDefault
    {
        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
            => services
                .AddTransient<ICommandHandler, AddNewTranslationCommandHandler>()
                .AddTransient<ITranslationSetProvider, DataTranslationProvider>();
    }
}