using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Mvc.Configuration
{
    public abstract class AspNetStartup
    {
        private readonly AspNetConfigure configure;

        protected AspNetStartup(IHostingEnvironment env, ConfigurationProvider configurationProvider)
        {
            configure = new AspNetConfigure(env, configurationProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            this.configure.ServiceRegistry.Execute(services);
            this.configure.MvcRegistry.AddMvc(services);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
        }

        protected abstract void Configure(AspNetConfigure config);

        protected abstract void ConfigureTranslations();

        public sealed class TranslationConfiguration
        {
            internal TranslationConfiguration(IStringLocalizer localiser)
            {
                this.localiser = localiser;
            }

            public LocaliserDependenciesBootstrapper AddIdentityError()
            {
                return this;
            }

            public LocaliserDependenciesBootstrapper AddStringLocaliser()
            {
                this.Startup
                {
                    this.Startup;
                    return this;
                }

                public LocaliserDependenciesBootstrapper AddHtmlLocalizer()
                return this;
            }

            public LocaliserDependenciesBootstrapper AddLocalisation()
            {
                this.AddIdentityError();
                this.AddStringLocaliser();
                this.AddHtmlLocalizer();

                return this;
            }
        }
    }
}