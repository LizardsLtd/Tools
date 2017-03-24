using System;
using Microsoft.Extensions.DependencyInjection;
using TheLizzards.Mvc.Startup;

namespace TheLizzards.Mvc.Startup
{
    public static class ServicesExtension
    {
        public static IConfiguration AddScoped<TService, TImplementation>(this IConfiguration startup)
                where TService : class
                where TImplementation : class, TService
            => startup.AddServices(x => x.AddScoped<TService, TImplementation>());

        public static IConfiguration AddTransient<TService, TImplementation>(this IConfiguration startup)
                where TService : class
                where TImplementation : class, TService
            => startup.AddServices(x => x.AddTransient<TService, TImplementation>());

        public static IConfiguration AddTransient<TService>(this IConfiguration startup, TService implementation)
                where TService : class
            => startup.AddServices(x => x.AddTransient(serviceCollection => implementation));

        public static IConfiguration AddSingleton<TService>(this IConfiguration startup)
               where TService : class
           => startup.AddSingleton<TService, TService>();

        public static IConfiguration AddSingleton<TService, TImplementation>(this IConfiguration startup)
                where TService : class
                where TImplementation : class, TService
            => startup.AddServices(x => x.AddSingleton<TService, TImplementation>());

        public static IConfiguration AddSingleton<TService>(this IConfiguration startup, Func<IServiceProvider, TService> implementationFactory)
                where TService : class
            => startup.AddServices(x => x.AddSingleton(implementationFactory));

        public static IConfiguration AddSingleton<TService>(this IConfiguration startup, TService implementation)
                where TService : class
            => startup.AddServices(x => x.AddSingleton(implementation));
    }
}