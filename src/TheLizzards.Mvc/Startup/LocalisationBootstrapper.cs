using Microsoft.Extensions.DependencyInjection;

namespace TheLizzards.Mvc.Startup
{
	public static class LocalisationBootstrapper
	{
		public static IConfiguration UseViewLocalisation(this IConfiguration startup)
			=> startup
				.ForMvcOption()
				.AddMvcBuilderAction(options => options.AddViewLocalization());
	}
}