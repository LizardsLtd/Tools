using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Picums.Mvc.Configuration;
using Picums.Mvc.Configuration.Defaults;

namespace Picums.Mvc.TestApp
{
    public class Startup : AspNetStartup
    {
        public Startup(IHostingEnvironment env) : base(env)
        {
            this.ApplyDefault<SetLocalisation>();
            //this.ApplyDefault<LocalisationByDatabase>();
            this.ApplyDefault<DataStorage>("Picums.Localisation");
            this.ApplyDefault<UseStaticFiles>();
        }

        protected override void ConfigureLogging(ILoggerFactory loggerFactory)
            => loggerFactory
                .AddConsole(ConfigurationRoot.GetSection("Logging"))
                .AddDebug();

        protected override void AddConfigurationBuilderDetails(ConfigurationBuilder provider)
            => provider
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json");

        protected override void AddMvcService(MvcConfigurator config)
            => config.Routes.AddRoute(routes => routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));

        protected override void ConfigureDevelopmentEnviroment(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
        }
    }
}