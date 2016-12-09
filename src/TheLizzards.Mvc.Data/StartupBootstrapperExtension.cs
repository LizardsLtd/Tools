using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TheLizzards.Data.CQRS.Contracts;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.CQRS.Entities;

namespace TheLizzards.Mvc.Startup
{
	public static class StartupBootstrapperExtension
	{
		public static IConfiguration AddAutmaticDetectionCQRS(
			this IConfiguration startup
			, string assemblyName)
		{
			var assembly = Assembly.Load(new AssemblyName(assemblyName));
			return startup.AddAutmaticDetectionCQRS(assembly);
		}

		public static IConfiguration AddAutmaticDetectionCQRS(
				this IConfiguration startup
				, Assembly assembly)
			=> startup.AddServices(services
				=> services.AutomaticDetectCQRSElements(assembly));

		public static IConfiguration AddCommandBus(
				this IConfiguration startup)
			=> startup.AddServices(services
					=> services.AddDefaultCQRSItems());

		public static IConfiguration AddDataContext(
				this IConfiguration startup)
			=> startup.AddConfiguration((app, e, lf)
				=> app
					.ApplicationServices
					.GetService<IDataContextInitialiser>()
					?.Initialise());

		private static void AddDefaultCQRSItems(this IServiceCollection services)
			=> services.AddSingleton<ICommandBus, CommandBus>();

		private static void AutomaticDetectCQRSElements(
				this IServiceCollection services
				, Assembly assembly)
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
					.ForTypesImplementingInterface<ICommandHandler>()
					.AddAsInterface<ICommandHandler>()
				.DiscoverImplementation()
					.ForAssembly(assembly)
					.IncludeClassesOnly()
					.ForTypesImplementingInterface<IsQuery>()
					.AsImplementedInterfaces()
				.DiscoverImplementation()
					.ForAssembly(assembly)
					.IncludeClassesOnly()
					.ForTypesImplementingInterface<IStory>()
					.AsImplementedInterfaces();
	}
}