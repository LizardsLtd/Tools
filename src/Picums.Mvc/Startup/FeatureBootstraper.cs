using Microsoft.AspNetCore.Mvc.Razor;
using Picums.Mvc.FeatureSlices;

namespace Picums.Mvc.Startup
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