using Microsoft.Extensions.DependencyInjection;
using Picums.GeoCoding;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class GeoCodingDefault : BasicDefault
    {
        protected override void ConfigureServices(IServiceCollection services, object[] arguments)
        {
            services.AddTransient<IGeocodingService, GoogleGeocodingService>();
        }
    }
}