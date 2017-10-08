using System;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;
using Picums.Data.Domain;

namespace Picums.Mvc.Localisation
{
    [DebuggerDisplay("{CultureName}:{TranslationKey}:{Value}")]
    public sealed class TranslationItem : IAggregateRoot, IEquatable<TranslationItem>
    {
        /// <summary>Record Constructor</summary>
        /// <param name="cultureName"><see cref="CultureName"/></param>
        /// <param name="translationKey"><see cref="TranslationKey"/></param>
        /// <param name="value"><see cref="Value"/></param>
        public TranslationItem(Guid id, CultureInfo culture, string translationKey, string value)
        {
            this.Id = id;
            this.CultureName = GetCultureName(culture);
            this.TranslationKey = translationKey;
            this.Value = value;
        }

        [JsonConstructor]
        public TranslationItem(Guid id, string cultureName, string translationKey, string value)
            : this(id, new CultureInfo(cultureName), translationKey, value) { }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; }

        public string CultureName { get; }

        public string TranslationKey { get; }

        public string Value { get; }

        public TranslationItem AddValue(string value)
            => new TranslationItem(this.Id, this.CultureName, this.TranslationKey, value);

        public bool CompareKeys(CultureInfo culture, string key)
            => this.GetHashCode() == $"{this.GetCultureName(culture)}:{key}".GetHashCode();

        public override bool Equals(object obj) => this.Equals(obj as TranslationItem);

        public bool Equals(TranslationItem other)
            => this.GetHashCode() == other?.GetHashCode()
             && this.Value == other?.Value;

        public override int GetHashCode() => $"{CultureName}:{TranslationKey}".GetHashCode();

        private string GetCultureName(CultureInfo culture) => culture.TwoLetterISOLanguageName;
    }
}