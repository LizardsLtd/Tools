using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Picums.Mvc.Configuration.Defaults;

namespace Picums.Mvc.Localisation.Configuration.Defaults
{
    public sealed class LocaliseUsingConfigurationDefault : BasicDefault
    {
        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
        {
            services.AddTransient(_ => this.GetTranslationSetProvider());
        }

        private ITranslationSetProvider GetTranslationSetProvider()
            => new JsonTransaltionProvider(this.Configuration.GetSection("Translations"));
    }
}