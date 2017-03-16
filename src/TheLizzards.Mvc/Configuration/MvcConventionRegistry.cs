using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class MvcConventionRegistry
    {
        private readonly List<Action<MvcOptions>> options;

        internal MvcConventionRegistry()
        {
            options = new List<Action<MvcOptions>>(25);
        }

        public MvcConventionRegistry AddApplicationConvention<T>() where T : IApplicationModelConvention, new()
           => Add(new T());

        public MvcConventionRegistry Add(IApplicationModelConvention convention)
        {
            options.Add(options => options.Conventions.Add(convention));
            return this;
        }

        public MvcConventionRegistry AddControllerConvention<T>() where T : IControllerModelConvention, new()
            => Add(new T());

        public MvcConventionRegistry Add(IControllerModelConvention convention)
        {
            options.Add(options => options.Conventions.Add(convention));
            return this;
        }

        internal void Execute(MvcOptions mvcOption) => options.ForEach(x => x(mvcOption));
    }
}