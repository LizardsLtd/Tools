using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Picums.Mvc.Localisation
{
    public sealed class JsonTransaltionProvider : ITranslationSetProvider
    {
        private readonly TranslationSet translationSet;

        public JsonTransaltionProvider(IConfigurationSection configuration)
        {
            this.translationSet = new TranslationSet(ConvertToTransationData(configuration));
        }

        public TranslationSet GetTranslationSet() => this.translationSet;

        public IEnumerable<TranslationItem> ConvertToTransationData(IConfigurationSection configuration)
            => configuration
                .GetChildren()
                .SelectMany(x => ConverToFlatDictionary(x.GetChildren()));

        private IEnumerable<TranslationItem> ConverToFlatDictionary(
                IEnumerable<IConfigurationSection> itemsToProcess)
            => itemsToProcess
                .Aggregate(
                    Enumerable.Empty<IConfigurationSection>(),
                    (seed, item) => seed.Union(GetAlldescendantsAndSelf(item)))
                .Where(x => x.Value != null)
                .Select(x => new
                {
                    Culture = ExtractCulture(x.Path),
                    Key = ExtractKey(x.Path),
                    Value = x.Value,
                })
                .Select(x => new
                {
                    Culture = new CultureInfo(x.Culture),
                    Key = x.Key.Replace($"{x.Culture}.", string.Empty),
                    Value = x.Value,
                })
                .Select(x => new TranslationItem(
                    Guid.NewGuid()
                    , x.Culture
                    , x.Key
                    , x.Value));

        private IEnumerable<IConfigurationSection> GetAlldescendantsAndSelf(IConfigurationSection section)
        {
            IEnumerable<IConfigurationSection> result = new[] { section };

            foreach (var test in section.GetChildren())
            {
                result = result.Union(GetAlldescendantsAndSelf(test));
            }

            return result;
        }

        private string ExtractCulture(string source) => source.Split(':')[1];

        private string ExtractKey(string source)
            => source
                .Substring(source.IndexOf(":"))
                .TrimStart(':')
                .Replace(':', '.');
    }
}