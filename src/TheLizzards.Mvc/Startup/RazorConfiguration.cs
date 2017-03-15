using Microsoft.AspNetCore.Mvc.Razor;
using TheLizzards.Mvc.FeatureSlices;

namespace TheLizzards.Mvc.Startup
{
    public sealed class RazorConfiguration
    {
        public RazorConfiguration AddFeatureSlice(this IConfiguration startup)
            => startup.ConfigureOption<RazorViewEngineOptions>(options
                => new ViewLocationFormatsUpdater(options)
                    .UpdateViewLocations()
                    .AddExtender());
    }
}