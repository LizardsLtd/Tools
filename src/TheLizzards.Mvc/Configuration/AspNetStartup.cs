using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Mvc.Configuration
{
    public abstract partial class AspNetStartup
    {
        protected readonly AspNetConfigure configure;

        protected AspNetStartup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(env.ContentRootPath);
            AddConfigurationBuilderDetails(configurationBuilder);
            configure = new AspNetConfigure(env, configurationBuilder);
        }

        protected IConfigurationRoot ConfigurationRoot => this.configure.Configuration;
        protected IHostingEnvironment Enviroment => configure.Enviroment;

        public void ConfigureServices(IServiceCollection services)
        {
            AddServices(this.configure.ServiceRegistry);
            AddMvcService(this.configure.MvcRegistry);

            configure.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            ConfigureLogging(loggerFactory);

            configure.ConfigureAll(app, loggerFactory);
        }

        protected virtual void AddConfigurationBuilderDetails(ConfigurationBuilder provider)
        {
        }

        protected virtual void AddServices(ServiceRegistry services)
        {
        }

        protected virtual void AddMvcService(MvcRegistry services)
        {
        }

        protected virtual void ConfigureLogging(ILoggerFactory loggerFactory)
        {
        }

        protected virtual void ConfigureLocalisation()
        {
        }
    }
}