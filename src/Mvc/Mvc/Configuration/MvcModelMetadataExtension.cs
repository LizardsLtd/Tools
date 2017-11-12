using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Picums.Mvc.Configuration
{
    public static class MvcModelMetadataExtension
    {
        public static Configurator<MvcOptions> AddMetadataProvider<TMetadata>(this Configurator<MvcOptions> options)
                where TMetadata : IMetadataDetailsProvider, new()
            => options.AddMetadataProvider(new TMetadata());

        public static Configurator<MvcOptions> AddMetadataProvider(this Configurator<MvcOptions> options, IMetadataDetailsProvider provider)
            => options.Add(option => option.ModelMetadataDetailsProviders.Insert(0, provider));
    }
}