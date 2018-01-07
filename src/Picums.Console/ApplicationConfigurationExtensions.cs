using Microsoft.Extensions.Configuration;

namespace Picums.Console
{
    public static class ApplicationConfigurationExtensions
    {
        public static Application<TApplication> AddCommandLineArguments<TApplication>(
                    this Application<TApplication> app,
                    string[] args)
            where TApplication : class, IRunnable
        {
            app.Configuration.AddCommandLine(args);

            return app;
        }

        public static Application<TApplication> AddJsonFile<TApplication>(
                this Application<TApplication> app,
                string path,
                bool optional = true)
            where TApplication : class, IRunnable
        {
            app.Configuration.AddJsonFile(path, optional);

            return app;
        }
    }
}