namespace Microsoft.Extensions.DependencyInjection
{
	public interface TypeFiler
	{
		TypeAssigment ForTypesImplementingInterface<TInterface>();

		TypeFiler IncludeClassesOnly();
	}
}