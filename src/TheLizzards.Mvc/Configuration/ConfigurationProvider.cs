using Microsoft.Extensions.Configuration;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class ConfigurationProvider
    {
        private readonly ConfigurationBuilder configurationBuilder;

        internal ConfigurationProvider(string basePath)
        {
            this.configurationBuilder = new ConfigurationBuilder();
            this.configurationBuilder.SetBasePath(basePath);
        }

        public ConfigurationProvider AddEnvironmentVariablesToConfiguration()
        {
            this.configurationBuilder.AddEnvironmentVariables();
            return this;
        }

        public ConfigurationProvider AddJsonFile(string fileName)
        {
            this.configurationBuilder.AddJsonFile(fileName);
            return this;
        }

        internal IConfigurationRoot Build()
        {
            return this.configurationBuilder.Build();
        }
    }
}