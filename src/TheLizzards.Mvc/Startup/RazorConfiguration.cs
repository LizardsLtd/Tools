using Microsoft.AspNetCore.Mvc.Razor;
using TheLizzards.Mvc.FeatureSlices;

namespace TheLizzards.Mvc.Startup
{
    public static class RazorViewBootstrapper
    {
        public static IConfiguration AddFeatureSlice(this IConfiguration startup)
            => startup.ConfigureOption<RazorViewEngineOptions>(options
                => new ViewLocationFormatsUpdater(options)
                    .UpdateViewLocations()
                    .AddExtender());
    }
}