using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Picums.Mvc.Localisation;
using Picums.Mvc.Localisation.Services;
using Picums.Mvc.Middleware;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class SetLocalisation : BasicDefault
    {
        private CultureStore cultureStore;

        protected override void Configure()
        {
            this.cultureStore = new CultureStore(this.Configuration);
        }

        protected override void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env, IEnumerable<object> arguments)
        {
            app.UseRequestLocalization(
                   new RequestLocalizationOptions
                   {
                       RequestCultureProviders = new List<IRequestCultureProvider>
                       {
                            new QueryStringRequestCultureProvider(),
                            new CookieRequestCultureProvider(),
                            new AcceptLanguageHeaderRequestCultureProvider(),
                       },
                       SupportedCultures = this.cultureStore.AvailableCultures,
                       SupportedUICultures = this.cultureStore.AvailableCultures,
                       DefaultRequestCulture = this.cultureStore.RequestCulture,
                   });
            app.UseMiddleware<CultureCookieSetterMiddleware>();

            var localisator = app.ApplicationServices.GetRequiredService<IStringLocalizer>();

            this.Mvc.Options.AddMetadataProvider(new DisplayAttributeLocalisationProvider(localisator));
            this.Mvc.Options.AddMetadataProvider(new RequiredValueAttributeLocalisationProvider(localisator));
            this.Mvc.Options.AddMetadataProvider(new ValidationAttributeLocalisationProvider(localisator));
        }

        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
            => services
                .AddSingleton(this.cultureStore)
                .AddSingleton<IdentityErrorDescriber, LocalisedIdentityErrorDescriber>()
                .AddSingleton<IStringLocalizer, ConfigurableStringLocalizer>()
                .AddSingleton<IHtmlLocalizer, HtmlLocalizer>();
    }
}