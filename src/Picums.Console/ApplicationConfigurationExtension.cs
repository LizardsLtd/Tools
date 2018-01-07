using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Picums.Console
{
    public static class ApplicationConfigurationExtension
    {
        public static Application<TApplication> AddConsoleLogging<TApplication>(this Application<TApplication> app)
            where TApplication : class, IRunnable
        {
            app.ServiceCollection.AddSingleton(GetLoggerFactory());
            app.ServiceCollection.AddLogging();

            return app;
        }

        private static object GetLoggerFactory()
            => new LoggerFactory()
                .AddConsole()
                .AddDebug();
    }
}