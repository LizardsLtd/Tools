using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class AspNetConfigure
    {
        internal readonly IHostingEnvironment env;

        internal AspNetConfigure(IHostingEnvironment env, ConfigurationProvider configurationProvider)
        {
            this.env = env;

            Configuration = configurationProvider.Build();

            ServiceRegistry = new ServiceRegistry();
            MvcRegistry = new MvcRegistry();
            AspRegistry = new AspRegistry();
            TranslationRegistry = new TranslationRegistry();
        }

        public ServiceRegistry ServiceRegistry { get; }

        public MvcRegistry MvcRegistry { get; }

        public AspRegistry AspRegistry { get; }

        public TranslationRegistry TranslationRegistry { get; }

        public IConfigurationRoot Configuration { get; }

        internal void ConfigureAll(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            AspRegistry.Configure(app, env, loggerFactory);
            MvcRegistry.UseMvc();
        }
    }
}