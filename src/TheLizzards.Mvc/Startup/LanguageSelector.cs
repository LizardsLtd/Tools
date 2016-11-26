using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace TheLizzards.Mvc.Startup
{
	public sealed class LanguageSelector : ConfigurationBase
	{
		private CultureInfo defaultLanguage;
		private List<CultureInfo> availableLanguages;
		private IConfigurationRoot configuration;

		internal LanguageSelector(
			 IConfiguration startup
			, IConfigurationRoot configuration)
				: base(startup)
		{
			this.configuration = configuration;
		}

		public LanguageSelector SetDefaultLanguage(string selector)
		{
			this.defaultLanguage
				= new CultureInfo(
					this.configuration[selector]);
			return this;
		}

		public LanguageSelector SetAvailableLanguages(string selector)
		{
			this.availableLanguages
				 = this.configuration
					.GetSection(selector)
					.GetChildren()
					.Select(x => x.Value)
					.Select(x => new CultureInfo(x))
					.ToList();
			return this;
		}

		public UseConfigurationRoot UseIn()
			=> new UseConfigurationRoot(
				this.Startup
				, this.defaultLanguage
				, this.availableLanguages
				, this.configuration);
	}
}