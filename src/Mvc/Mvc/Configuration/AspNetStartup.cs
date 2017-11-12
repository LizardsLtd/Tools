using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Picums.Mvc.Configuration.Defaults;

namespace Picums.Mvc.Configuration
{
    public abstract class AspNetStartup
    {
        private readonly StartupConfigurations configuration;

        protected AspNetStartup(IHostingEnvironment env, IConfiguration configuration)
        {
            this.configuration = new StartupConfigurations(env, configuration);
        }

        public IConfiguration Configuration => this.configuration.Configuration;

        public IHostingEnvironment Environment => this.configuration.Environment;

        public AspNetStartup ConfigureOptions<TOption>(Action<TOption> configure)
            where TOption : class
        {
            this.configuration.Services.Configure(configure);

            return this;
        }

        public AspNetStartup ConfigureOptions<TOption>(Action<IConfiguration, TOption> configure)
            where TOption : class
        {
            this.configuration.Services.Configure<TOption>(options => configure(this.Configuration, options));

            return this;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            this.configuration.MVC.AddMvc(services);
            this.configuration.Razor.Use(services);
            this.configuration.Services.Use(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            this.configuration.ASP.Use(app, this.Environment);
            this.configuration.MVC.Use(app);
        }

        public void ApplyDefault<TDefault>(params object[] arguments)
                where TDefault : IDefault, new()
            => this.ApplyDefault(new TDefault(), arguments);

        public void ApplyDefault(IDefault @default, params object[] arguments)
        {
            this.configuration.Apply(@default, arguments);
        }

        protected virtual void AddConfigurationBuilderDetails(ConfigurationBuilder provider)
        {
        }
    }
}