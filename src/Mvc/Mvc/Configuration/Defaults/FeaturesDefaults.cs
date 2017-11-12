using System.Collections.Generic;
using Picums.Mvc.FeatureSlices;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class FeaturesDefaults : IDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            host.MVC.Options.AddControllerConvention<FeatureConvention>();

            host.Razor.Options(options
                => new ViewLocationFormatsUpdater(options)
                    .UpdateViewLocations()
                    .AddExtender());
        }
    }
}