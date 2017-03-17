using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Mvc.Configuration
{
    public abstract partial class AspNetStartup
    {
        protected readonly AspNetConfigure configure;

        protected AspNetStartup(IHostingEnvironment env)
        {
            var configurationProvider = new ConfigurationProvider(env.ContentRootPath);
            StaticSiteConfiguration(configurationProvider);
            configure = new AspNetConfigure(env, configurationProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddServices(this.configure.ServiceRegistry);
            AddMvcService(this.configure.MvcRegistry);

            this.configure.ServiceRegistry.Execute(services);
            this.configure.MvcRegistry.AddMvc(services);

            this.configure.TranslationRegistry.AddTranslationBasedItems(services);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            configure.ConfigureAll(app, loggerFactory);
        }

        protected abstract void StaticSiteConfiguration(ConfigurationProvider provider);

        protected abstract void AddServices(ServiceRegistry services);

        protected abstract void AddMvcService(MvcRegistry services);

        protected abstract void ConfigureLogging();

        protected abstract void ConfigureLocalisation();
    }
}