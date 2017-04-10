using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Picums.Localisation;
using Picums.Localisation.Data;
using Picums.Mvc.Localisation;
using Picums.Mvc.Middleware;

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
            host.Apply<MiddlewareDefault<CultureCookieSetterMiddleware>>();
            host.ASP.Add(this.ConfigureRequestLocalisation(cultureStore));
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

        private Action<IApplicationBuilder, IHostingEnvironment> ConfigureRequestLocalisation(CultureStore cultureStore)
        {
            var defaultLanguage = new RequestCulture(cultureStore.DefaultCulture);
            var availableLanguages = cultureStore.AvailableCultures.ToList();

            return (app, env) => app.UseRequestLocalization(
                   new RequestLocalizationOptions
                   {
                       RequestCultureProviders = new List<IRequestCultureProvider>
                       {
                            new AcceptLanguageHeaderRequestCultureProvider()
                            , new QueryStringRequestCultureProvider()
                            , new CookieRequestCultureProvider()
                       },
                       SupportedCultures = availableLanguages,
                       SupportedUICultures = availableLanguages,
                       DefaultRequestCulture = defaultLanguage,
                   });

            //        UseMiddlewareCultureRecognition(
            //defaultLanguage
            //,
            //, new CookieRequestCultureProvider()
            //, new QueryStringRequestCultureProvider()
            //, new AcceptLanguageHeaderRequestCultureProvider());
        }
    }
}