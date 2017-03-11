using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TheLizzards.Mvc.Localisation
{
    public sealed class TranslationSet
    {
        private readonly HashSet<(string, string, string)> translationData;

        public TranslationSet(IEnumerable<(string, string, string)> items)
        {
            translationData = new HashSet<(string, string, string)>(items, new CultureAndKeyComparer());
        }

        public string GetTranslation(CultureInfo culture, string key)
            => this.translationData
                .Where(x => x.Item1 == culture.TwoLetterISOLanguageName)
                .Where(x => x.Item2 == key)
                .Select(x => x.Item3)
                .DefaultIfEmpty(key)
                .FirstOrDefault();

        public TranslationSet Merge(TranslationSet additionalData)
            => new TranslationSet(translationData.Union(additionalData.translationData));

        internal IEnumerable<(string, string)> GetAll(CultureInfo culture)
            => this.translationData
                .Where(x => x.Item1 == culture.TwoLetterISOLanguageName)
                .Select(x => (x.Item2, x.Item3));
    }
}