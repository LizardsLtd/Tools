using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheLizzards.Mvc.Configuration;

namespace TheLizzards.Mvc.TestApp
{
    public class Startup : AspNetStartup
    {
        public Startup(IHostingEnvironment env) : base(env)
        {
        }

        protected override void AddMvcService(MvcRegistry services)
        {
        }

        protected override void AddServices(ServiceRegistry services)
        {

        }

        protected override void ConfigureApplication(IApplicationBuilder app)
        {
            app.UseStaticFiles();
        }

        protected override void ConfigureLocalisation()
        {
        }

        protected override void ConfigureLogging(ILoggerFactory loggerFactory)
            => loggerFactory
                .AddConsole(ConfigurationRoot.GetSection("Logging"))
                .AddDebug();

        protected override void AddConfigurationBuilderDetails(ConfigurationBuilder provider)
            => provider
                .AddEnvironmentVariables()
                .AddJsonFile("Configuration.json")
                .AddJsonFile("Translations.json");

        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables();
        //    Configuration = builder.Build();
        //}

        //public IConfigurationRoot Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // Add framework services.
        //    services.AddMvc();
        //}

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //    }

        //    app.UseStaticFiles();

        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "default",
        //            template: "{controller=Home}/{action=Index}/{id?}");
        //    });
        //}
    }
}