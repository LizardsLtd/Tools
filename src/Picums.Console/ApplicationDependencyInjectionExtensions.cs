using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Picums.Console
{
    public static class ApplicationDependencyInjectionExtensions
    {
         public static Application<TApplication> AddSingleton<TApplication, TService, TImplementation>(
                this Application<TApplication> app)
            where TApplication : class, IRunnable
            where TService : class
            where TImplementation : class, TService
        {
            app.ServiceCollection.AddSingleton<TService, TImplementation>();

            return app;
        }

        public static Application<TApplication> AddSingleton<TApplication, TService, TImplementation>(
                this Application<TApplication> app,
                Func<IServiceProvider, TImplementation> implementationFactory)
            where TApplication : class, IRunnable
            where TService : class
            where TImplementation : class, TService
        {
            app.ServiceCollection.AddSingleton<TService>(services => implementationFactory(services));

            return app;
        }

        public static Application<TApplication> AddSingleton<TApplication, TService, TImplementation>(
                this Application<TApplication> app,
                TImplementation implementation)
            where TApplication : class, IRunnable
            where TService : class
            where TImplementation : class, TService
        {
            app.ServiceCollection.AddSingleton<TService>(services => implementation);

            return app;
        }   
        
        public static Application<TApplication> AddTransient<TApplication, TService, TImplementation>(
                this Application<TApplication> app)
            where TApplication : class, IRunnable
            where TService : class
            where TImplementation : class, TService
        {
            app.ServiceCollection.AddTransient<TService, TImplementation>();

            return app;
        }

        public static Application<TApplication> AddTransient<TApplication, TService, TImplementation>(
                this Application<TApplication> app,
                Func<IServiceProvider, TImplementation> implementationFactory)
            where TApplication : class, IRunnable
            where TService : class
            where TImplementation : class, TService
        {
            app.ServiceCollection.AddTransient<TService>(services => implementationFactory(services));

            return app;
        }

        public static Application<TApplication> AddTransient<TApplication, TService, TImplementation>(
                this Application<TApplication> app,
                TImplementation implementation)
            where TApplication : class, IRunnable
            where TService : class
            where TImplementation : class, TService
        {
            app.ServiceCollection.AddTransient<TService>(services => implementation);

            return app;
        }

        public static Application<TApplication> AddOptions<TApplication, TOptions>(
                this Application<TApplication> app, Action<TOptions> factory)
            where TApplication : class, IRunnable
            where TOptions : class
        {
            app.ServiceCollection.Configure(factory);

            return app;
        }

        public static Application<TApplication> AddOptions<TApplication, TOptions>(
                this Application<TApplication> app,
                Func<IConfigurationRoot, Action<TOptions>> factory)
           where TApplication : class, IRunnable
           where TOptions : class
        {
            app.ServiceCollection.Configure(factory(app.ConfigurationRoot.Value));

            return app;
        }
    }
}
