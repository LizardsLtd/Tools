using Microsoft.Extensions.Configuration;

namespace Picums.Mvc.Startup
{
    public static class ConfigurationBootstrapperCreator
    {
        public static ConfigurationBootstrapper UseBasePath(this IConfiguration startup, string basePath)
            => new ConfigurationBootstrapper(startup, basePath);
    }

    public sealed class ConfigurationBootstrapper : ConfigurationBase
    {
        private readonly string basePath;
        private readonly ConfigurationBuilder configurationBuilder;

        internal ConfigurationBootstrapper(IConfiguration startup, string basePath)
            : base(startup)
        {
            this.basePath = basePath;
            this.configurationBuilder = new ConfigurationBuilder();
            this.configurationBuilder.SetBasePath(this.basePath);
        }

        public UseConfigurationBootstraper UseFor()
            => new UseConfigurationBootstraper(this.Startup, this.configurationBuilder.Build());
    }
}