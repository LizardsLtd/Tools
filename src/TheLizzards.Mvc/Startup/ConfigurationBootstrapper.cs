using Microsoft.Extensions.Configuration;

namespace TheLizzards.Mvc.Startup
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

		public ConfigurationBootstrapper AddEnvironmentVariablesToConfiguration()
		{
			this.configurationBuilder.AddEnvironmentVariables();
			return this;
		}

		public ConfigurationBootstrapper AddJsonFile(string fileName)
		{
			this.configurationBuilder.AddJsonFile(fileName);
			return this;
		}

		public LanguageSelector UseFor()
			=> new LanguageSelector(
				this.Startup
				, this.configurationBuilder.Build());
	}
}