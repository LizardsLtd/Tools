using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TheLizzards.I18N;

namespace TheLizzards.Mvc.Startup
{
    public sealed class UseConfigurationRoot : ConfigurationBase
    {
        private readonly IConfigurationRoot configuration;
        private readonly Dictionary<string, string> properties;
        private readonly CultureStore cultureStore;
        private IStringLocalizer stringLocaliser;

        internal UseConfigurationRoot(IConfiguration startup, IConfigurationRoot configuration, CultureStore cultureStore)
                : base(startup)
        {
            this.configuration = configuration;
            this.properties = new Dictionary<string, string>();
            this.cultureStore = cultureStore;
        }

        public UseConfigurationRoot UseMiddlewareCultureRecognition()
        {
            var defaultLanguage = new RequestCulture(this.cultureStore.DefaultCulture);

            return UseMiddlewareCultureRecognition(
                defaultLanguage
                , this.cultureStore.AvailableCultures.ToList()
                , new CookieRequestCultureProvider()
                , new QueryStringRequestCultureProvider()
                , new AcceptLanguageHeaderRequestCultureProvider());
        }

        public UseConfigurationRoot UseMiddlewareCultureRecognition(
            RequestCulture defaultCulture
            , IList<CultureInfo> availableLanguages
            , params IRequestCultureProvider[] culturePrviders)
        {
            this.Startup.AddConfiguration(
                (app, e, lf) => app.UseRequestLocalization(
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
                        DefaultRequestCulture = defaultCulture,
                    }));
            return this;
        }

        public LocaliserDependenciesBootstrapper InitialiseTranslation(string translationSelector)
        {
            return new LocaliserDependenciesBootstrapper(this.Startup);
        }

        public UseConfigurationRoot Configure<TOptions>(Action<IConfigurationRoot, TOptions> configureOptions)
            where TOptions : class
        {
            this.Startup.ConfigureOption<TOptions>(x => configureOptions(this.configuration, x));

            return this;
        }
    }
}