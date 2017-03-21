using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheLizzards.Mvc.Configuration;
using TheLizzards.Mvc.Configuration.Defaults;

namespace TheLizzards.Mvc.TestApp
{
    public class Startup : AspNetStartup
    {
        public Startup(IHostingEnvironment env) : base(env)
        {
            this.ApplyDefault<SetLocalisation>();
            //this.ApplyDefault<LocalisationByDatabase>();
            this.ApplyDefault<DataStorage>("TheLizzards.I18N");
        }

        protected override void ConfigurationApp(IApplicationBuilder app)
            => app.UseStaticFiles();

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