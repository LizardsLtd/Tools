using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
	public interface TypeQuery
	{
		TypeFiler ForAssembly(Assembly assembly);
	}
}