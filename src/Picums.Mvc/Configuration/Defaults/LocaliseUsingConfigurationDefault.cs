using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Picums.Localisation.Data.Services;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class LocaliseUsingConfigurationDefault : BasicDefault
    {
        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
        {
            services.AddTransient(_ => this.GetTranslationSetProvider());
        }

        private ITranslationSetProvider GetTranslationSetProvider()
            => new JsonTransaltionProvider(this.ConfigurationRoot.GetSection("Translations"));
    }
}