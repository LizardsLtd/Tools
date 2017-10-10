using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Picums.Mvc.Localisation.Services
{
    internal sealed class JsonTranslationProvider
    {
        private readonly IConfigurationSection configuration;
        private readonly CultureInfo defaultCulture;

        public JsonTranslationProvider(IConfigurationSection configuration)
        {
            this.configuration = configuration;
        }

        public TranslationSet GetTranslationSet()
            => new TranslationSet(this.ConvertToTransationData(this.configuration), this.defaultCulture);

        public IEnumerable<TranslactionItem> ConvertToTransationData(IConfigurationSection configuration)
            => configuration
                .GetChildren()
                .SelectMany(x => this.ConverToFlatDictionary(x.GetChildren()));

        private IEnumerable<TranslactionItem> ConverToFlatDictionary(IEnumerable<IConfigurationSection> itemsToProcess)
            => itemsToProcess
                .Aggregate(
                    Enumerable.Empty<IConfigurationSection>(),
                    (seed, item) => seed.Union(this.GetAlldescendantsAndSelf(item)))
                .Where(x => x.Value != null)
                .Select(x => new
                {
                    Culture = this.ExtractCulture(x.Path),
                    Key = this.ExtractKey(x.Path),
                    Value = x.Value,
                })
                .Select(x => new
                {
                    Culture = new CultureInfo(x.Culture),
                    Key = x.Key.Replace($"{x.Culture}.", string.Empty),
                    Value = x.Value,
                })
                .Select(x => new TranslactionItem(
                    x.Culture
                    , x.Key
                    , x.Value));

        private IEnumerable<IConfigurationSection> GetAlldescendantsAndSelf(
            IConfigurationSection section)
        {
            IEnumerable<IConfigurationSection> result = new[] { section };

            foreach (var test in section.GetChildren())
            {
                result = result.Union(this.GetAlldescendantsAndSelf(test));
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