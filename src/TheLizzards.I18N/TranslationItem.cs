namespace TheLizzards.Mvc.Localisation
{
    public struct TranslationItem
    {
        /// <summary>Record Constructor</summary>
        /// <param name="cultureName"><see cref="CultureName"/></param>
        /// <param name="translationKey"><see cref="TranslationKey"/></param>
        /// <param name="value"><see cref="Value"/></param>
        public TranslationItem(string cultureName, string translationKey, string value)
        {
            CultureName = cultureName;
            TranslationKey = translationKey;
            Value = value;
        }

        public string CultureName { get; }

        public string TranslationKey { get; }

        public string Value { get; }

        public override int GetHashCode() => $"{CultureName}:{TranslationKey}".GetHashCode();
    }
}