using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Mvc.Configuration
{
    public abstract partial class AspNetStartup
    {
        protected AspNetStartup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(env.ContentRootPath);
            this.AddConfigurationBuilderDetails(configurationBuilder);

            this.ConfigurationRoot = configurationBuilder.Build();
            this.Environment = env;
            this.MVC = new MvcRegistry();
            this.ASP = new AspRegistry();
        }

        public MvcRegistry MVC { get; }

        public AspRegistry ASP { get; }

        public IHostingEnvironment Environment { get; }

        public IConfigurationRoot ConfigurationRoot { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            this.AddMvcService(this.MVC);

            this.MVC.AddMvc(services);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            this.ASP.Configure(app, Environment, loggerFactory);
            this.MVC.UseMvc(app);
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

        protected virtual void ApplyDefault<TDefault>() where TDefault :
    }
}