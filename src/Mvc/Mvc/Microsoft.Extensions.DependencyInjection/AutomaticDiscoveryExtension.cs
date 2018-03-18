namespace Microsoft.Extensions.DependencyInjection
{
	public static class AutomaticDiscoveryExtension
	{
		public static TypeQuery DiscoverImplementation(this IServiceCollection services)
			=> new AutomaticDiscovery(services);
	}
}