using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using TheLizzards.I18N.Data;
using TheLizzards.Mvc.Localisation;

namespace TheLizzards.Mvc.Startup
{
    public sealed class LocaliserDependenciesBootstrapper : ConfigurationBase
    {
        public LocaliserDependenciesBootstrapper(IConfiguration startup)
                : base(startup)
        {
            this.Startup.AddSingleton<IStringLocalizer, ConfigurableStringLocalizer>();
        }

        public LocaliserDependenciesBootstrapper AddHtmlLocalizer()
        {
            this.Startup.AddTransient<IHtmlLocalizer, HtmlLocalizer>();
            return this;
        }

        public LocaliserDependenciesBootstrapper AddIdentityError()
        {
            this.Startup
                .AddServices(services
                    => services.AddScoped<IdentityErrorDescriber, LocalisedIdentityErrorDescriber>());
            return this;
        }

        //public LocaliserDependenciesBootstrapper AddDataAnnotationsLocalization(bool useViewLcalisation = true)
        //{
        //    this.Startup
        //        .ForMvcOption()
        //        .AddMvcBuilderAction(options
        //            => options.AddDataAnnotationsLocalization(DataAnnotationOptions));
        //    return this;
        //}
    }
}