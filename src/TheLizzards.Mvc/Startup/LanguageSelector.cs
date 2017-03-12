using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using TheLizzards.I18N;
using TheLizzards.I18N.Data;
using TheLizzards.I18N.Data.Services;

namespace TheLizzards.Mvc.Startup
{
    public sealed class LanguageSelector : ConfigurationBase
    {
        private readonly string databaseName;
        private IConfigurationRoot configuration;

        internal LanguageSelector(
             IConfiguration startup
            , IConfigurationRoot configuration
            , string databaseName)
                : base(startup)
        {
            this.configuration = configuration;
            this.databaseName = databaseName;
        }

        public LanguageSelector InitialiseCultureStore(string selector)
        {
            var cultureStore = new CultureStore(
                this.GetDefaultLanguage($"{selector}:Default")
                , this.GetAvailableLanguages($"{selector}:Available"));

            this.Startup.AddSingleton(cultureStore);
            this.Startup.AddSingleton<ConfigurableStringLocalizer>();

            return this;
        }

        public LanguageSelector InitialiseJsonBasedTranslations(string selector)
        {
            this.Startup.AddTransient<ITranslationSetProvider>(new JsonTransaltionProvider(this.configuration.GetSection(selector)));
            return this;
        }

        public LanguageSelector InitialiseDataStorageBasedTranslations(string databaseName)
        {
            this.Startup.AddTransient<ITranslationSetProvider, DataTransaltionProvider>();
            return this;
        }

        public UseConfigurationRoot UseIn()
                => new UseConfigurationRoot(
                    this.Startup
                    , this.configuration);

        private CultureInfo GetDefaultLanguage(string selector) => new CultureInfo(this.configuration[selector]);

        private IEnumerable<CultureInfo> GetAvailableLanguages(string selector)
            => this.configuration
                .GetSection(selector)
                .GetChildren()
                .Select(x => x.Value)
                .Select(x => new CultureInfo(x))
                .ToList();
    }
}