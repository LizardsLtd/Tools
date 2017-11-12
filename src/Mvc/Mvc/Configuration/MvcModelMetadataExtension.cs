using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Picums.Mvc.Configuration
{
    public static class MvcModelMetadataExtension
    {
        public static Configurator<MvcOptions> AddDispalyMetadata<TMetadata>(this Configurator<MvcOptions> options)
                where TMetadata : IDisplayMetadataProvider, new()
            => options.AddDispalyMetadata(new TMetadata());

        public static Configurator<MvcOptions> AddDispalyMetadata(this Configurator<MvcOptions> options, IDisplayMetadataProvider provider)
            => options.Add(option => option.ModelMetadataDetailsProviders.Add(provider));
    }
}