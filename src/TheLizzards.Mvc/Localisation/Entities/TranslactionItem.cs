using System.Globalization;

namespace TheLizzards.Mvc.Localisation.Entities
{
	public struct TranslactionItem
	{
		public readonly CultureInfo Culture;

		public readonly string Key;

		public readonly string Value;

		public TranslactionItem(CultureInfo culture, string key, string value)
		{
			this.Culture = culture;
			this.Key = key;
			this.Value = value;
		}
	}
}