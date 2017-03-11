using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TheLizzards.Mvc.Localisation
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
                .Where(x => x.CultureName == culture.TwoLetterISOLanguageName)
                .Where(x => x.TranslationKey == key)
                .Select(x => x.Value)
                .DefaultIfEmpty(key)
                .FirstOrDefault();

        internal IEnumerable<(string, string)> GetAll(CultureInfo culture)
            => this.translationData
                .Where(x => x.CultureName == culture.TwoLetterISOLanguageName)
                .Select(x => (x.TranslationKey, x.Value));
    }
}