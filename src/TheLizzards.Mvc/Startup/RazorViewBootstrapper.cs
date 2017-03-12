using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
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

        public static IConfiguration UseViewLocalisation(this IConfiguration startup)
            => startup
                .ForMvcOption()
                .AddMvcBuilderAction(options => options.AddViewLocalization());
    }
}