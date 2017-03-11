using System.Globalization;

namespace TheLizzards.Mvc.Localisation
{
    public struct TranslactionItem
    {
        public TranslactionItem(CultureInfo culture, string key, string value)
        {
            this.Culture = culture;
            this.Key = key;
            this.Value = value;
        }

        public CultureInfo Culture { get; }

        public string Key { get; }

        public string Value { get; }
    }
}