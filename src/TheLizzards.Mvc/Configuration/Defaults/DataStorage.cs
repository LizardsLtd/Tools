using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TheLizzards.Data.CQRS;
using TheLizzards.Data.CQRS.Commands;
using TheLizzards.Data.CQRS.DataAccess;

namespace TheLizzards.Mvc.Configuration.Defaults
{
    public sealed class DataStorage : IDefault
    {
        public void Apply(StartupConfigurations host, params object[] arguments)
        {
            arguments
                .Cast<string>()
                .Select(assemblyName => Assembly.Load(new AssemblyName(assemblyName)))
                .ToList()
                .ForEach(assembly => host.Services.Add(AutomaticDetection(assembly)));

            host.Services.Add(services => services.AddSingleton<ICommandBus, CommandBus>());
        }

        private Action<IServiceCollection> AutomaticDetection(Assembly assembly)
            => services => services
                .DiscoverImplementation()
                    .ForAssembly(assembly)
                    .IncludeClassesOnly()
                    .ForTypesImplementingInterface<IDataContext>()
                    .AddAsInterface<IDataContext>()
                .DiscoverImplementation()
                    .ForAssembly(assembly)
                    .IncludeClassesOnly()
                    .ForTypesImplementingInterface<IDataContextInitialiser>()
                    .AddAsInterface<IDataContextInitialiser>()
                .DiscoverImplementation()
                    .ForAssembly(assembly)
                    .IncludeClassesOnly()
                    .ForTypesImplementingInterface<ICommandHandler>()
                    .AddAsInterface<ICommandHandler>()
                .DiscoverImplementation()
                    .ForAssembly(assembly)
                    .IncludeClassesOnly()
                    .ForTypesImplementingInterface<IsQuery>()
                    .AsSelf()
                .DiscoverImplementation()
                    .ForAssembly(assembly)
                    .IncludeClassesOnly()
                    .ForTypesImplementingInterface<IStory>()
                    .AsImplementedInterfaces();
    }
}