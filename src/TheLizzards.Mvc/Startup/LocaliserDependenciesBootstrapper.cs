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
        private readonly Func<IStringLocalizer> localiserFactory;

        public LocaliserDependenciesBootstrapper(IConfiguration startup, Func<IStringLocalizer> localiserFactory)
                : base(startup)
        {
            this.localiserFactory = localiserFactory;
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
                    var localiser = this.localiserFactory();
                    options.ModelMetadataDetailsProviders.Add(new DisplayAttributeLocalisationProvider(localiser));
                 });
            return this;
        }

        public LocaliserDependenciesBootstrapper AddValidationAttributeProvider()
        {
            this.Startup
                .ForMvcOption()
                .AddMvcOption(options =>                 
                {
                    var localiser = this.localiserFactory();
                    options.ModelMetadataDetailsProviders.Add(new ValidationAttributeLocalisationProvider(localiser));
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
