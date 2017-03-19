using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Mvc.Configuration
{
    public abstract partial class AspNetStartup
    {
        private MvcRegistry mvcRegistry;

        private IHostingEnvironment environment;

        protected AspNetStartup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(env.ContentRootPath);
            this.AddConfigurationBuilderDetails(configurationBuilder);

            this.ConfigurationRoot = configurationBuilder.Build();
            this.environment = env;
            this.mvcRegistry = new MvcRegistry();
        }

        protected IConfigurationRoot ConfigurationRoot { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            this.AddMvcService(this.mvcRegistry);

            this.mvcRegistry.AddMvc(services);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            this.ConfigureAsp(app, environment, loggerFactory);
            this.mvcRegistry.UseMvc(app);
        }

        protected virtual void AddConfigurationBuilderDetails(ConfigurationBuilder provider)
        {
        }

        protected virtual void AddServices(IServiceCollection services)
        {
        }

        protected virtual void AddMvcService(MvcRegistry services)
        {
        }

        protected virtual void ConfigureAsp(
            IApplicationBuilder app
            , IHostingEnvironment env
            , ILoggerFactory loggerFactory)
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