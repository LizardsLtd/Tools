using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class AspNetConfigure
    {
        private readonly IHostingEnvironment env;

        internal AspNetConfigure(IHostingEnvironment env, ConfigurationProvider configurationProvider)
        {
            this.env = env;

            Configuration = configurationProvider.Build();

            ServiceRegistry = new ServiceRegistry();
            MvcRegistry = new MvcRegistry();
            AspRegistry = new AspRegistry();
        }

        public ServiceRegistry ServiceRegistry { get; }

        public MvcRegistry MvcRegistry { get; }

        public AspRegistry AspRegistry { get; }

        public IConfigurationRoot Configuration { get; }
    }
}