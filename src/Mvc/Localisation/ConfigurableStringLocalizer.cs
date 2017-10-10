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

        public ConfigurableStringLocalizer(IEnumerable<ITranslationSetProvider> translationData, CultureStore culture)
            : this(translationData, culture.CurrentCulture) { }

        public ConfigurableStringLocalizer(IEnumerable<ITranslationSetProvider> translationData, CultureInfo culture)
            : this(
                  translationData
                    .Select(x => x.GetTranslationSet())
                    .Aggregate((prev, next) => prev.Merge(next)),
                  culture)
        { }

        private ConfigurableStringLocalizer(TranslationSet translationData, CultureInfo culture)
        {
            this.translationData = translationData;
            this.culture = culture;
        }

        public LocalizedString this[string name]
            => new LocalizedString(name, this.GetTranslatedString(name));

        public LocalizedString this[string name, params object[] arguments]
            => new LocalizedString(name, this.GetTranslatedString(name, arguments));

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => this.translationData.GetAll(this.culture).Select(x => new LocalizedString(x.Item1, x.Item2));

        public IStringLocalizer WithCulture(CultureInfo culture)
            => new ConfigurableStringLocalizer(this.translationData, culture);

        private string GetTranslatedString(string name, params object[] arguments)
            => string.Format(this.translationData.GetTranslation(this.culture, name), arguments);
    }
}