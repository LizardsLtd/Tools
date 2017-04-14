using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Picums.Mvc.Startup
{
    public abstract class ConfigurationBase : IConfiguration
    {
        protected ConfigurationBase(IConfiguration startup)
        {
            Startup = startup;
        }

        protected IConfiguration Startup { get; }

        public IConfiguration AddConfiguration(Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory> action)
            => Startup.AddConfiguration(action);

        public IConfiguration AddServices(Action<IServiceCollection> action)
            => Startup.AddServices(action);

        public IConfiguration ConfigureOption<TOption>(Action<TOption> action) where TOption : class
            => Startup.ConfigureOption(action);
    }
}