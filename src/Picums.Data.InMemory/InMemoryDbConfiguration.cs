using System.Collections.Generic;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Data.InMemory
{
    public sealed class InMemoryDbConfiguration : IDatabaseConfiguration
    {
        private readonly Dictionary<string, string> configurationItems;

        public InMemoryDbConfiguration(string database, IDictionary<string, string> collection)
        {
            this.configurationItems = new Dictionary<string, string>(collection)
            {
                { "database", database },
            };
        }

        public string GetSetting(string key)
            => this.configurationItems.ContainsKey(key)
                ? this.configurationItems[key]
                : string.Empty;
    }
}