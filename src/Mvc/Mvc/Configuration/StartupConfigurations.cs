using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Picums.Mvc.Configuration.Defaults;

namespace Picums.Mvc.Configuration
{
    public sealed class StartupConfigurations
    {
        /// <summary>Record Constructor</summary>
        /// <param name="mVC"><see cref="MVC"/></param>
        /// <param name="aSP"><see cref="ASP"/></param>
        /// <param name="razor"><see cref="Razor"/></param>
        /// <param name="environment"><see cref="Environment"/></param>
        /// <param name="configurationRoot"><see cref="Configuration"/></param>
        public StartupConfigurations(IHostingEnvironment environment, IConfiguration configuration)
        {
            this.MVC = new MvcConfig();
            this.ASP = new AspConfigurator();
            this.Razor = new RazorConfig();
            this.Services = new ServicesConfigurator();
            this.Environment = environment;
            this.Configuration = configuration;
        }

        public MvcConfig MVC { get; }

        public AspConfigurator ASP { get; }

        public RazorConfig Razor { get; }

        public ServicesConfigurator Services { get; }

        public IHostingEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public void Apply<TDefault>(params object[] arguments) where TDefault : IDefault, new()
            => this.Apply(new TDefault(), arguments);

        internal void Apply(IDefault @default, params object[] arguments) => @default.Apply(this, arguments);
    }
}