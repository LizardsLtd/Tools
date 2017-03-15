using Microsoft.AspNetCore.Mvc.Razor;
using TheLizzards.Mvc.FeatureSlices;

namespace TheLizzards.Mvc.Startup
{
    public static class FeatureBootstraper
    {
        public static IConfiguration AddFeatureSliceConfiguration(this IConfiguration startup)
            => startup.ConfigureOption<RazorViewEngineOptions>(options
                => new ViewLocationFormatsUpdater(options)
                    .UpdateViewLocations()
                    .AddExtender());
    }
}