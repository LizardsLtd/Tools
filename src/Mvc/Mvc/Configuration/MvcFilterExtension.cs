using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Picums.Mvc.Configuration
{
    public static class MvcFilterExtension
    {
        public static Configurator<MvcOptions> AddFilterMetadata<TFilterMetadata>(this Configurator<MvcOptions> options)
                where TFilterMetadata : IFilterMetadata, new()
            => options.AddFilterMetadata(new TFilterMetadata());

        public static Configurator<MvcOptions> AddFilterMetadata(this Configurator<MvcOptions> options, IFilterMetadata filter)
            => options.Add(option => option.Filters.Insert(0, filter));
    }
}