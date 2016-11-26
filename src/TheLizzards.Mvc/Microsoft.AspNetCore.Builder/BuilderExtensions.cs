using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TheLizzards.Mvc.Configuration;

namespace Microsoft.AspNetCore.Builder
{
	public static class BuilderExtensions
	{
		public static IApplicationBuilder UseDefaultLocalisation(this IApplicationBuilder app)
		{
			var config = app
				.ApplicationServices
				.GetService<IOptions<LanguageConfigurationOptions>>()
				.Value;

			var availableCulturesList = config.AvailableCultures.ToList();

			return app
				.UseRequestLocalization(
					new RequestLocalizationOptions
					{
						RequestCultureProviders = new List<IRequestCultureProvider>
						{
							new AcceptLanguageHeaderRequestCultureProvider()
							, new QueryStringRequestCultureProvider()
							, new CookieRequestCultureProvider()
						},
						SupportedCultures = availableCulturesList,
						SupportedUICultures = availableCulturesList,
						DefaultRequestCulture = new RequestCulture(config.DefaultCulture),
					});
		}
	}
}