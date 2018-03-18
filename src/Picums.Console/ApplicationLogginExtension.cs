using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Picums.Console
{
    public static class ApplicationLogginExtension
    {
        public static Application<TApplication> EnableLoggerFactory<TApplication>(this Application<TApplication> app)
         where TApplication : class, IRunnable
        {
            app.ServiceCollection.AddLogging();

            return app;
        }

        public static Application<TApplication> ConfigureLoggerFactory<TApplication>(
                this Application<TApplication> app,
                Func<ILoggerFactory, IConfigurationRoot, ILoggerFactory> configurationFactory)
            where TApplication : class, IRunnable
        {
            app.ServiceCollection.AddSingleton(
                configurationFactory(GetLoggerFactory(), app.ConfigurationRoot.Value));

            return app;
        }

        private static LoggerFactory GetLoggerFactory()
            => new LoggerFactory();
    }
}