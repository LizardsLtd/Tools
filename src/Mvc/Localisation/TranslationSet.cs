using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Picums.Mvc.Localisation
{
    public sealed class TranslationSet
    {
        private readonly HashSet<TranslationItem> translationData;

        public TranslationSet(IEnumerable<TranslationItem> items)
        {
            translationData = new HashSet<TranslationItem>(items, new CultureAndKeyComparer());
        }

        public TranslationSet Merge(TranslationSet additionalData)
            => new TranslationSet(translationData.Union(additionalData.translationData));

        public string GetTranslation(CultureInfo culture, string key)
            => this.translationData
                .Where(x => CompareWithCaseIgnored(x.CultureName, culture.TwoLetterISOLanguageName))
                .Where(x => CompareWithCaseIgnored(x.TranslationKey, key))
                .Select(x => x.Value)
                .DefaultIfEmpty(key)
                .FirstOrDefault();

        internal IEnumerable<(string, string)> GetAll(CultureInfo culture)
            => this.translationData
                .Where(x => CompareWithCaseIgnored(x.CultureName, culture.TwoLetterISOLanguageName))
                .Select(x => (x.TranslationKey, x.Value));

        internal IEnumerable<(string, string)> GetAll(object currentCulture)
            => this.GetAll(currentCulture as CultureInfo);

        private bool CompareWithCaseIgnored(string first, string second)
                    => string.Equals(first, second, StringComparison.OrdinalIgnoreCase);
    }
}