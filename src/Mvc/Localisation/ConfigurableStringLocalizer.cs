using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace Picums.Mvc.Localisation
{
    public sealed class ConfigurableStringLocalizer : IStringLocalizer
    {
        private readonly CultureInfo culture;
        private readonly TranslationSet translationData;

        public ConfigurableStringLocalizer(IEnumerable<ITranslationSetProvider> translationData)
            : this(
                  translationData.Select(x => x.GetTranslationSet()).Aggregate((prev, next) => prev.Merge(next)),
                  null)
        { }

        private ConfigurableStringLocalizer(TranslationSet translationData, CultureInfo culture)
        {
            this.translationData = translationData;
            this.culture = culture;
        }

        private CultureInfo Culture => this.culture ?? CultureInfo.CurrentUICulture;

        public LocalizedString this[string name]
            => new LocalizedString(name, this.GetTranslatedString(name));

        public LocalizedString this[string name, params object[] arguments]
            => new LocalizedString(name, this.GetTranslatedString(name, arguments));

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => this.translationData.GetAll(this.Culture).Select(x => new LocalizedString(x.Item1, x.Item2));

        public IStringLocalizer WithCulture(CultureInfo culture)
            => new ConfigurableStringLocalizer(this.translationData, culture);

        private string GetTranslatedString(string name, params object[] arguments)
            => string.Format(this.translationData.GetTranslation(this.Culture, name), arguments);
    }
}