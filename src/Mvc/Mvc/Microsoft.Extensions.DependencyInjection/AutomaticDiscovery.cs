using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public sealed class AutomaticDiscovery : TypeQuery, TypeFiler, TypeAssigment
    {
        private readonly IServiceCollection services;
        private IEnumerable<Type> types;

        internal AutomaticDiscovery(IServiceCollection services)
        {
            this.services = services;
        }

        public TypeFiler ForAssembly(Assembly assembly)
        {
            this.types = assembly.GetTypes();
            return this;
        }

        public TypeFiler IncludeClassesOnly()
        {
            this.types = this.types.Where(type => type.GetTypeInfo().IsClass);
            return this;
        }

        public TypeAssigment ForTypesImplementingInterface<TInterface>()
        {
            this.types = this.types.Where(this.IsImplementing<TInterface>);
            return this;
        }

        public TypeAssigment ForTypesImplementingInterface(Type desiredBaseType)
        {
            this.types = this.types.Where(type => this.IsImplementing(type, desiredBaseType));
            return this;
        }

        public IServiceCollection AddAsInterface<TInterface>()
        {
            var typeOfInterface = typeof(TInterface);
            this.types.ToList().ForEach(x => this.services.AddScoped(typeOfInterface, x));
            return this.services;
        }

        public IServiceCollection AsImplementedInterfaces()
        {
            this.types.ToList().ForEach(AddAsImplementedInterfaces);
            return this.services;
        }

        public IServiceCollection AsSelf()
        {
            this.types.ToList().ForEach(x => this.services.AddScoped(x));
            return this.services;
        }

        private void AddAsImplementedInterfaces(Type type)
            => type.GetInterfaces()
                .ToList()
                .ForEach(@interface => this.services.AddScoped(@interface, type));

        private bool IsImplementing<TInterface>(Type type)
            => this.IsImplementing(type, typeof(TInterface));

        private bool IsImplementing(Type type, Type isImplmenting)
            => type.GetInterfaces().Contains(isImplmenting);
    }
}