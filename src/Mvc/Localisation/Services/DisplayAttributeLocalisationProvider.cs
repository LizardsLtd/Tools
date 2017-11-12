using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Localization;

namespace Picums.Mvc.Localisation.Services
{
    public sealed class DisplayAttributeLocalisationProvider : IDisplayMetadataProvider
    {
        private IStringLocalizer localiser;

        public DisplayAttributeLocalisationProvider(IStringLocalizer localiser)
        {
            this.localiser = localiser;
        }

        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
            => context
                .PropertyAttributes
                ?.Where(attribute => attribute is DisplayAttribute)
                .Cast<DisplayAttribute>()
                .ToList()
                .ForEach(x => x.Name = this.Translate(x, context));

        private string Translate(DisplayAttribute attribute, DisplayMetadataProviderContext context)
        {
            var name = attribute.Name ?? $"{context.Key.ContainerType.Name}.{context.Key.Name}";

            return this.localiser.GetString(name);
        }
    }
}