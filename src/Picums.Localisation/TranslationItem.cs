using System;
using Picums.Data.Domain;
using System.Globalization;
using System.Diagnostics;

namespace Picums.Localisation.Data
{
    [DebuggerDisplay("{CultureName}:{TranslationKey}:{Value}")]
    public sealed class TranslationItem : IAggregateRoot
    {
        /// <summary>Record Constructor</summary>
        /// <param name="cultureName"><see cref="CultureName"/></param>
        /// <param name="translationKey"><see cref="TranslationKey"/></param>
        /// <param name="value"><see cref="Value"/></param>
        public TranslationItem(CultureInfo cultureName, string translationKey, string value)
        {
            this.Id = Guid.NewGuid();
            CultureName = cultureName.TwoLetterISOLanguageName;
            TranslationKey = translationKey;
            Value = value;
        }

        public Guid Id { get; }

        public string CultureName { get; }

        public string TranslationKey { get; }

        public string Value { get; }

        public bool Compare(CultureInfo culture, string key )
            => this.GetHashCode() == $"{culture}:{key}".GetHashCode();

        public override int GetHashCode() => $"{CultureName}:{TranslationKey}".GetHashCode();


    }
}