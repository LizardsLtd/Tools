using TheLizzards.Mvc.FeatureSlices;

namespace TheLizzards.Mvc.Configuration.Defaults
{
    public sealed class FeaturesDefaults : IDefault
    {
        public void Apply(IDefaultsHost host)
        {
            host.MVC.Conventions.AddControllerConvention<FeatureConvention>();

            host.Razor.Options(option => )

            //     public static IConfiguration AddFeatureSlice(this IConfiguration startup)
            //=> startup.ConfigureOption<RazorViewEngineOptions>(options
            //    => new ViewLocationFormatsUpdater(options)
            //        .UpdateViewLocations()
            //        .AddExtender());
        }
    }
}