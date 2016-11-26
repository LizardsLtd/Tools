using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Mvc.Startup
{
	public static class ServerBootstrapper
	{
		public static IConfiguration Defaults(this IConfiguration startup)
			=> startup
				.UseStaticPages();

		public static IConfiguration ConfigureLogging(
			this IConfiguration startup
			, Action<ILoggerFactory> addLogginDetails)
		{
			startup.AddConfiguration((a, e, loggerFactory) => addLogginDetails(loggerFactory));

			return startup;
		}

		public static IConfiguration UseStaticPages(this IConfiguration startup)
		{
			startup.AddConfiguration((app, e, lf) => app.UseStaticFiles());
			return startup;
		}

		public static IConfiguration EnableDebugging(
			this IConfiguration startup,
			bool force = false)
		{
			startup.AddConfiguration(
				(app, environment, lg) =>
					 {
						 if (environment.IsDevelopment() || force)
						 {
							 app.UseDeveloperExceptionPage();
						 }
					 });

			return startup;
		}

		public static IConfiguration UseBrowserLink(this IConfiguration startup)
		{
			startup.AddConfiguration(
				(app, environment, lg) =>
				{
					if (environment.IsDevelopment())
					{
						app.UseBrowserLink();
					}
				});

			return startup;
		}

		public static IConfiguration AddErrorHandler(
			this IConfiguration startup
			, string errorHandleUrl)
		{
			startup.AddConfiguration((app, enviroment, lf) =>
				  {
					  if (enviroment.IsDevelopment())
					  {
						  app.UseExceptionHandler(errorHandleUrl);
					  }
				  });
			return startup;
		}
	}
}