using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace TheLizzards.Mvc.Startup
{
    public sealed class UseConfigurationRoot : ConfigurationBase
    {
        private readonly IConfigurationRoot configuration;
        private readonly Dictionary<string, string> properties;
        private readonly List<CultureInfo> availableLanguages;
        private readonly CultureInfo defaultLanguage;

        internal UseConfigurationRoot(
            IConfiguration startup
            , IConfigurationRoot configuration)
                : base(startup)
        {
            this.configuration = configuration;
            this.properties = new Dictionary<string, string>();
        }

        public UseConfigurationRoot UseMiddlewareCultureRecognition()
        {
            var defaultLanguage = new RequestCulture(this.defaultLanguage);

            return UseMiddlewareCultureRecognition(
                defaultLanguage
                , availableLanguages
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
            var translation = new Lazy<IStringLocalizer>();

            return new LocaliserDependenciesBootstrapper(
                this.Startup
                , translation);
        }

        public UseConfigurationRoot Configure<TOptions>(Action<IConfigurationRoot, TOptions> configureOptions)
            where TOptions : class
        {
            this.Startup.ConfigureOption<TOptions>(x => configureOptions(this.configuration, x));

            return this;
        }
    }
}