using System;
using Picums.Data.Domain;

namespace Picums.I18N.Data
{
    public sealed class TranslationItem : IAggregateRoot
    {
        /// <summary>Record Constructor</summary>
        /// <param name="cultureName"><see cref="CultureName"/></param>
        /// <param name="translationKey"><see cref="TranslationKey"/></param>
        /// <param name="value"><see cref="Value"/></param>
        public TranslationItem(string cultureName, string translationKey, string value)
        {
            this.Id = Guid.NewGuid();
            CultureName = cultureName;
            TranslationKey = translationKey;
            Value = value;
        }

        public Guid Id { get; }

        public string CultureName { get; }

        public string TranslationKey { get; }

        public string Value { get; }

        public override int GetHashCode() => $"{CultureName}:{TranslationKey}".GetHashCode();
    }
}