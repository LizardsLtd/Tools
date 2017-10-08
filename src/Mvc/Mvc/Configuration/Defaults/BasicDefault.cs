using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Picums.Mvc.Configuration.Defaults
{
    public abstract class BasicDefault : IDefault
    {
        protected IConfigurationRoot ConfigurationRoot { get; private set; }

        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            this.ConfigurationRoot = host.ConfigurationRoot;
            host.ASP.Add((app, env, lg) => this.ConfigureApp(app, env, lg, arguments));
            host.Services.Add(services => this.ConfigureServices(services, arguments));
        }

        protected virtual void ConfigureApp(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IEnumerable<object> arguments)
        {
        }

        protected virtual void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
        {
        }
    }
}