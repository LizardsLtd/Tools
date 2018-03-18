using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class CQRSDefaults : BasicDefault
    {
        protected override void ConfigureApp(
                IApplicationBuilder app,
                IHostingEnvironment env,
                IEnumerable<object> arguments)
            => app
                .ApplicationServices
                .GetService<IDataContextInitialiser>()
                .Initialise()
                .Wait();

        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
        {
            arguments
                .Cast<string>()
                .Select(assemblyName => Assembly.Load(new AssemblyName(assemblyName)))
                .ToList()
                .ForEach(assembly => this.AutomaticDetection(services, assembly));
        }

        private void AutomaticDetection(IServiceCollection services, Assembly assembly)
            => services
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
                    .ForTypesImplementingInterface(typeof(ICommandHandler))
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