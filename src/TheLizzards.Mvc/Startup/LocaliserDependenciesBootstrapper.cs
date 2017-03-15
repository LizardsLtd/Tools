using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using TheLizzards.I18N.Data;
using TheLizzards.Mvc.Localisation;

namespace TheLizzards.Mvc.Startup
{
    public sealed class LocaliserDependenciesBootstrapper : ConfigurationBase
    {
        public LocaliserDependenciesBootstrapper(IConfiguration startup) : base(startup)
        {
        }

        public LocaliserDependenciesBootstrapper AddIdentityError()
        {
            this.Startup.AddScoped<IdentityErrorDescriber, LocalisedIdentityErrorDescriber>();
            return this;
        }

        public LocaliserDependenciesBootstrapper AddStringLocaliser()
        {
            this.Startup.AddSingleton<IStringLocalizer, ConfigurableStringLocalizer>();
            return this;
        }

        public LocaliserDependenciesBootstrapper AddHtmlLocalizer()
        {
            this.Startup.AddTransient<IHtmlLocalizer, HtmlLocalizer>();
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