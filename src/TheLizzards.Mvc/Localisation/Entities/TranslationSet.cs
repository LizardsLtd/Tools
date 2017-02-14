using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheLizzards.Mvc.Localisation.Entities
{
	public sealed class TranslationSet
	{
		private readonly Dictionary<string, Dictionary<string, string>> translationData;
		private readonly string defaultCulture;

		public TranslationSet(IEnumerable<TranslactionItem> items, CultureInfo defaultCulture)
		{
			translationData = new Dictionary<string, Dictionary<string, string>>(
				StringComparer.CurrentCultureIgnoreCase);

			this.AddItemsToTranslationSet(items);

			this.defaultCulture = defaultCulture?.TwoLetterISOLanguageName ?? "pl";
		}

		public Dictionary<string, string> GetTranslationSet(CultureInfo culture)
			=> this.translationData.ContainsKey(culture.TwoLetterISOLanguageName)
				? this.translationData[culture.TwoLetterISOLanguageName]
				: this.translationData[this.defaultCulture];

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