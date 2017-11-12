using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace Picums.Mvc.Configuration
{
    public static class MvcConventionExtension
    {
        public static Configurator<MvcOptions> AddApplicationConvention<T>(this Configurator<MvcOptions> options)
                where T : IApplicationModelConvention, new()
            => options.AddApplicationConvention(new T());

        public static Configurator<MvcOptions> AddApplicationConvention(this Configurator<MvcOptions> options, IApplicationModelConvention convention)
            => options.Add(option => option.Conventions.Insert(0, convention));

        public static Configurator<MvcOptions> AddControllerConvention<T>(this Configurator<MvcOptions> options)
                where T : IControllerModelConvention, new()
            => options.AddControllerConvention(new T());

        public static Configurator<MvcOptions> AddControllerConvention(this Configurator<MvcOptions> options, IControllerModelConvention convention)
            => options.Add(option => option.Conventions.Add(convention));
    }
}