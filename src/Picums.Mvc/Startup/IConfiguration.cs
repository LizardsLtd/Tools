using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace TheLizzards.Mvc.Startup
{
    public interface IConfiguration
    {
        IConfiguration AddConfiguration(Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory> action);

        IConfiguration AddServices(Action<IServiceCollection> action);

        IConfiguration ConfigureOption<TOption>(Action<TOption> action) where TOption : class;
    }
}