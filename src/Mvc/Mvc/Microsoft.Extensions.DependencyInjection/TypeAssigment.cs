namespace Microsoft.Extensions.DependencyInjection
{
	public interface TypeAssigment
	{
		IServiceCollection AddAsInterface<TInterface>();

		IServiceCollection AsImplementedInterfaces();

		IServiceCollection AsSelf();
	}
}