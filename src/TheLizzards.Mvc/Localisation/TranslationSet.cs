using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheLizzards.Mvc.Localisation
{
    public sealed class TranslationSet
    {
        private readonly Dictionary<string, Dictionary<string, string>> translationData;

        public TranslationSet(IEnumerable<TranslactionItem> items, CultureInfo defaultCulture)
        {
            translationData = new Dictionary<string, Dictionary<string, string>>(
                StringComparer.CurrentCultureIgnoreCase);

            this.AddItemsToTranslationSet(items);
        }

        public string GetTranslation(CultureInfo culture, string key)
            => DoesDesiredCultureHasAnyTranslations(culture)
                ? GetTranslatedValue(culture, key)
                : key;

        private bool DoesDesiredCultureHasAnyTranslations(CultureInfo culture)
        {
            return this.translationData.ContainsKey(culture.TwoLetterISOLanguageName);
        }

        private string GetTranslatedValue(CultureInfo culture, string key)
            => this.translationData[culture.TwoLetterISOLanguageName][key];

        private void AddItemsToTranslationSet(IEnumerable<TranslactionItem> items)
        {
            foreach (var item in items)
            {
                if (!this.DoesTranslationSetExists(item.Culture.TwoLetterISOLanguageName))
                {
                    this.CreateNewCultureSet(item.Culture.TwoLetterISOLanguageName);
                }

                this.AddItemToTranslationSet(item);
            }
        }

        private bool DoesTranslationSetExists(string translationSetKey)
            => translationData.ContainsKey(translationSetKey);

        private void AddItemToTranslationSet(TranslactionItem item)
            => translationData[item.Culture.TwoLetterISOLanguageName].Add(item.Key, item.Value);

        private void CreateNewCultureSet(string translationSetKey)
            => translationData.Add(
                translationSetKey
                , new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase));
    }
}