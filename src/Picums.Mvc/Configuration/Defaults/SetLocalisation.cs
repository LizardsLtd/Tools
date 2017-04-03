using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Picums.Localisation;
using Picums.Localisation.Data;
using Picums.Localisation.Data.Services;
using Picums.Mvc.Localisation;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class SetLocalisation : IDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            var cultureStore = GetCultureStore(host.ConfigurationRoot);

            host.Services.Add(x => x.AddSingleton(cultureStore));
            host.Services.Add(x => x.AddSingleton<IStringLocalizer, ConfigurableStringLocalizer>());
            host.Services.Add(x => x.AddSingleton<IdentityErrorDescriber, LocalisedIdentityErrorDescriber>());
            host.Services.Add(x => x.AddSingleton<IHtmlLocalizer, HtmlLocalizer>());
            host.Services.Add(x => x.AddTransient<ITranslationSetProvider>(provider
                => new JsonTransaltionProvider(host.ConfigurationRoot.GetSection("translation"))));

            //host.Apply<MiddlewareDefault<CultureCookieSetterMiddleware>>();
        }

        private CultureStore GetCultureStore(IConfigurationRoot configurationRoot)
            => new CultureStore(
                this.GetDefaultLanguage(configurationRoot, $"Culture:Default")
                , this.GetAvailableLanguages(configurationRoot, $"Culture:Available"));

        private CultureInfo GetDefaultLanguage(IConfigurationRoot configurationRoot, string selector)
            => new CultureInfo(configurationRoot[selector]);

        private IEnumerable<CultureInfo> GetAvailableLanguages(IConfigurationRoot configurationRoot, string selector)
            => configurationRoot
                .GetSection(selector)
                .GetChildren()
                .Select(x => x.Value)
                .Select(x => new CultureInfo(x))
                .ToList();
    }
}