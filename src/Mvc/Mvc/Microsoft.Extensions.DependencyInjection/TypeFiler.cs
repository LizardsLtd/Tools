using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface TypeFiler
    {
        TypeAssigment ForTypesImplementingInterface<TInterface>();

        TypeAssigment ForTypesImplementingInterface(Type desiredBaseType);

        TypeFiler IncludeClassesOnly();
    }
}