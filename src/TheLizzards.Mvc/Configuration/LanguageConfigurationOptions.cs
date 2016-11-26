using System.Collections.Generic;
using System.Globalization;

namespace TheLizzards.Mvc.Configuration
{
	public sealed class LanguageConfigurationOptions
	{
		public CultureInfo DefaultCulture { get; private set; }

		public IList<CultureInfo> AvailableCultures { get; private set; }

		public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

		public void SetDefaultCulture(string culture)
			=> this.DefaultCulture = new CultureInfo(culture);

		public void SetAvailableCultures(IList<CultureInfo> cultures)
			=> this.AvailableCultures = cultures;
	}
}