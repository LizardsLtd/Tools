using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Picums.Mvc.Configuration.Defaults;
using Picums.Mvc.Middleware;

namespace Picums.Mvc.Configuration
{
    public abstract partial class AspNetStartup
    {
        private readonly StartupConfigurations configuration;

        protected AspNetStartup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(env.ContentRootPath);
            this.AddConfigurationBuilderDetails(configurationBuilder);

            this.configuration = new StartupConfigurations(env, configurationBuilder.Build());
        }

        public IHostingEnvironment Environment => this.configuration.Environment;

        public IConfigurationRoot ConfigurationRoot => this.configuration.ConfigurationRoot;

        public AspNetStartup ConfigureOptions<TOption>(Action<TOption> configure) where TOption : class
        {
            this.configuration.Services.Configure<TOption>(configure);

            return this;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            this.AddServices(services);
            this.AddMvcService(this.configuration.MVC);
            this.configuration.MVC.AddMvc(services);
            this.configuration.Razor.Use(services);
            this.configuration.Services.Use(services);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            this.ConfigureAsp(this.configuration.ASP);
            this.ConfigureLogging(loggerFactory);

            this.configuration.ASP.Use(app, Environment);
            this.configuration.MVC.Use(app);

            app.UseMiddleware<CultureCookieSetterMiddleware>();
        }

        public void ApplyDefault<TDefault>(params object[] arguments) where TDefault : IDefault, new()
            => this.ApplyDefault(new TDefault(), arguments);

        public void ApplyDefault(IDefault @default, params object[] arguments)
        {
            this.configuration.Apply(@default, arguments);
        }

        protected virtual void AddConfigurationBuilderDetails(ConfigurationBuilder provider)
        {
        }

        protected virtual void AddServices(IServiceCollection services)
        {
        }

        protected virtual void AddMvcService(MvcConfigurator config)
        {
        }

        protected virtual void ConfigureAsp(AspConfigurator config)
        {
        }

        protected virtual void ConfigureLogging(ILoggerFactory loggerFactory)
        {
        }
    }
}