using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;
using TheLizzards.I18N;
using TheLizzards.I18N.Data;
using TheLizzards.I18N.Data.Services;

namespace TheLizzards.Mvc.Startup
{
    public sealed class UseConfigurationBootstraper : ConfigurationBase
    {
        private readonly List<ITranslationSetProvider> providers;
        private IConfigurationRoot configuration;
        private CultureStore cultureStore;

        internal UseConfigurationBootstraper(IConfiguration startup, IConfigurationRoot configuration)
                : base(startup)
        {
            this.configuration = configuration;
            this.providers = new List<ITranslationSetProvider>(5);
        }

        public UseConfigurationBootstraper InitialiseCultureStore(string selector)
        {
            this.cultureStore = new CultureStore(
                this.GetDefaultLanguage($"{selector}:Default")
                , this.GetAvailableLanguages($"{selector}:Available"));

            this.Startup.AddSingleton(cultureStore);

            return this;
        }

        public UseConfigurationBootstraper InitialiseJsonBasedTranslations(string selector)
        {
            this.Startup.AddSingleton<ITranslationSetProvider>(
                new JsonTransaltionProvider(this.configuration.GetSection(selector)));

            return this;
        }

        public UseConfigurationBootstraper InitialiseDataStorageBasedTranslations(string databaseName)
        {
            this.Startup.AddSingleton<ITranslationSetProvider, DataTranslationProvider>();
            return this;
        }

        public UseConfigurationRoot UseIn()
        {
            this.Startup.AddSingleton<ConfigurableStringLocalizer>();
            return new UseConfigurationRoot(this.Startup, this.configuration, this.cultureStore);
        }

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
