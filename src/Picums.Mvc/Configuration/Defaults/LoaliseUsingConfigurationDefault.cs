using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Picums.Localisation.Data.Services;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class LocaliseUsingConfigurationDefault : BasicDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
        }

        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
        {
            services.TryAddSingleton<ITranslationSetProvider>(
                new JsonTransaltionProvider(this.ConfigurationRoot.GetSection("Translations")));
        }
    }
}