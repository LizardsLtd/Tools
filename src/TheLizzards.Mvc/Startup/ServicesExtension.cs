using Microsoft.Extensions.DependencyInjection;
using TheLizzards.Mvc.Startup;

namespace TheLizzards.Mvc.Startup
{
	public static class ServicesExtension
	{
		public static IConfiguration AddScoped<TService, TImplementation>(this IConfiguration startup)
				where TService : class
				where TImplementation : class, TService
			=> startup.AddServices(x => x.AddScoped<TService, TImplementation>());
	}
}