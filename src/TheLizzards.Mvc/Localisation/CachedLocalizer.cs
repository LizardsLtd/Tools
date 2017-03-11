using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace TheLizzards.Mvc.Localisation
{
    internal sealed class CachedLocalizer : IStringLocalizer
    {
        private readonly TranslationSet translationData;
        private readonly CultureInfo currentCulture;

        public CachedLocalizer(TranslationSet translationData)
        {
            this.translationData = translationData;
        }

        private Dictionary<string, string> CurrentTranslationSet
            => this.translationData.GetTranslationSet(CultureInfo.CurrentCulture);

        public LocalizedString this[string name, params object[] arguments]
            => GetString(name, arguments);

        public LocalizedString this[string name]
            => GetString(name);

        public LocalizedString GetString(string name)
            => GetString(name, new object[0]);

        public LocalizedString GetString(string name, params object[] arguments)
            => new LocalizedString(name, GetTranslatedString(name, arguments));

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => this.CurrentTranslationSet.Select(x => new LocalizedString(x.Key, x.Value));

        public IStringLocalizer WithCulture(CultureInfo culture)
            => new CachedLocalizer(this.translationData, culture);

        private string GetTranslatedString(string name)
            => GetTranslatedString(name, new object[0]);

        private string GetTranslatedString(string name, params object[] arguments)
            => string.Format(GetKeyOrDefault(name), arguments);

        private bool DoesKeyExists(string key)
            => CurrentTranslationSet.ContainsKey(key);

        private string GetKeyOrDefault(string key)
            => DoesKeyExists(key)
                ? CurrentTranslationSet[key]
                : key;
    }
}