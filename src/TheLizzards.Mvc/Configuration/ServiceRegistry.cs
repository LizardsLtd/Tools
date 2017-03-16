using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class ServiceRegistry
    {
        private readonly List<Action<IServiceCollection>> actions;

        internal ServiceRegistry()
        {
            actions = new List<Action<IServiceCollection>>(25);
        }

        public ServiceRegistry Add(Action<IServiceCollection> action)
        {
            actions.Add(action);
            return this;
        }

        public ServiceRegistry AddScoped<TService>() where TService : class
            => AddScoped<TService, TService>();

        public ServiceRegistry AddScoped<TService, TImplementation>() where TService : class
                where TImplementation : class, TService
            => Add(x => x.AddScoped<TService, TImplementation>());

        public ServiceRegistry AddTransient<TService>() where TService : class
            => AddTransient<TService, TService>();

        public ServiceRegistry AddTransient<TService, TImplementation>() where TService : class
                where TImplementation : class, TService
            => Add(x => x.AddTransient<TService, TImplementation>());

        public ServiceRegistry AddSingleton<TService>() where TService : class
           => AddSingleton<TService, TService>();

        public ServiceRegistry AddSingleton<TService, TImplementation>()
                where TService : class
                where TImplementation : class, TService
            => this.Add(x => x.AddSingleton<TService, TImplementation>());

        public ServiceRegistry AddSingleton<TService>(Func<IServiceProvider, TService> implementationFactory)
                where TService : class
            => this.Add(x => x.AddSingleton(implementationFactory));

        public ServiceRegistry AddSingleton<TService>(TService implementation) where TService : class
            => this.Add(x => x.AddSingleton(implementation));

        public ServiceRegistry Configure<TOption>(Action<TOption> action) where TOption : class
        {
            actions.Add(x => x.Configure(action));
            return this;
        }

        internal void Execute(IServiceCollection services)
        {
            actions.ForEach(action => action(services));
        }
    }
}