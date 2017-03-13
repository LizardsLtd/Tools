using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using TheLizzards.Mvc.Localisation;
using TheLizzards.Mvc.Localisation.Services;

namespace TheLizzards.Mvc.Startup
{
    public sealed class LocaliserDependenciesBootstrapper : ConfigurationBase
    {
        private readonly Lazy<IStringLocalizer> localiser;

        public LocaliserDependenciesBootstrapper(IConfiguration startup, Lazy<IStringLocalizer> localiser)
                : base(startup)
        {
            this.localiserFactory = localiser;
        }

        public LocaliserDependenciesBootstrapper AddHtmlLocalizer()
        {
            this.Startup.AddServices(services => services.AddTransient<IHtmlLocalizer, HtmlLocalizer>());
            return this;
        }

        public LocaliserDependenciesBootstrapper AddAllTranslators()
        {
            return this
                   .AddHtmlLocalizer()
                   .AddDisplayAttributeProvider()
                   .AddValidationAttributeProvider()
                   .AddIdentityError()
                   .AddDataAnnotationsLocalization();
        }

        public LocaliserDependenciesBootstrapper AddDisplayAttributeProvider()
        {
            this.Startup
                .ForMvcOption()
                .AddMvcOption(options => 
                {
                    var localiserInstance = this.localiser.Value;
                    options.ModelMetadataDetailsProviders.Add(new DisplayAttributeLocalisationProvider(localiserInstance));
                 });
            return this;
        }

        public LocaliserDependenciesBootstrapper AddValidationAttributeProvider()
        {
            this.Startup
                .ForMvcOption()
                .AddMvcOption(options =>                 
                {
                    var localiserInstance = this.localiser.Value;
                    options.ModelMetadataDetailsProviders.Add(new ValidationAttributeLocalisationProvider(localiserInstance));
                 });
            return this;
        }

        public LocaliserDependenciesBootstrapper AddIdentityError()
        {
            this.Startup
                .AddServices(services
                    => services.AddScoped<IdentityErrorDescriber, LocalisedIdentityErrorDescriber>());
            return this;
        }

        public LocaliserDependenciesBootstrapper AddDataAnnotationsLocalization(bool useViewLcalisation = true)
        {
            this.Startup
                .ForMvcOption()
                .AddMvcBuilderAction(options
                    => options.AddDataAnnotationsLocalization(DataAnnotationOptions));
            return this;
        }

        private void DataAnnotationOptions(MvcDataAnnotationsLocalizationOptions options)
        {
            options.DataAnnotationLocalizerProvider = (x, y) => this.localiser.Value;
        }
    }
}
