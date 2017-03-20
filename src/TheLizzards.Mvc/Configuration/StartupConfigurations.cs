using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class StartupConfigurations
    {
        /// <summary>Record Constructor</summary>
        /// <param name="mVC"><see cref="MVC"/></param>
        /// <param name="aSP"><see cref="ASP"/></param>
        /// <param name="razor"><see cref="Razor"/></param>
        /// <param name="environment"><see cref="Environment"/></param>
        /// <param name="configurationRoot"><see cref="ConfigurationRoot"/></param>
        public StartupConfigurations(IHostingEnvironment environment, IConfigurationRoot configurationRoot)
        {
            this.MVC = new MvcConfigurator();
            this.ASP = new AspConfigurator();
            this.Razor = new RazorOptions();
            this.Environment = environment;
            this.ConfigurationRoot = configurationRoot;
        }

        public MvcConfigurator MVC { get; }

        public AspConfigurator ASP { get; }

        public RazorOptions Razor { get; }

        public IHostingEnvironment Environment { get; }

        public IConfigurationRoot ConfigurationRoot { get; }
    }
}