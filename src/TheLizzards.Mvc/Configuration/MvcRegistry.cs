using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using TheLizzards.I18N.Data;
using TheLizzards.Mvc.Localisation;
using TheLizzards.Mvc.Navigation;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class MvcRegistry
    {
        public MvcRegistry()
        {
            Conventions = new MvcConventionRegistry();
            Models = new MvcModelRegistry();
            Options = new MvcOptionsRegistry();
            Filters = new FilterRegistry();
            Routes = new RouteConfigurator();
        }

        public MvcConventionRegistry Conventions { get; }

        public MvcModelRegistry Models { get; }

        public MvcOptionsRegistry Options { get; }

        public FilterRegistry Filters { get; }

        public RouteConfigurator Routes { get; }

        internal void AddMvc(IServiceCollection services)
        {
            var mvcBuilder = services
                .AddMvc(CreateMvcOptions)
                .AddViewLocalization();
            //.AddDataAnnotationsLocalization(this.DataAnnotationOptions);

            services
                .AddSingleton(AddNavigationItems(services, mvcBuilder))
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddScoped<IdentityErrorDescriber, LocalisedIdentityErrorDescriber>()
                .AddTransient<IHtmlLocalizer, HtmlLocalizer>()
                .AddSingleton<IStringLocalizer, ConfigurableStringLocalizer>();
        }

        internal void UseMvc()
        {
        }

        internal void Execute(IApplicationBuilder app)
        {
            //app.UseMvc(this.routeBuilder.BuildRoutes);
        }

        private void CreateMvcOptions(MvcOptions options)
        {
            Conventions.Execute(options);
            Models.Execute(options);
            Options.Execute(options);
            Filters.Execute(options);
        }

        private NavigationItems AddNavigationItems(IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            var navigationItems = new NavigationItems();

            Conventions.Add(new NavigationCreationConvention(navigationItems));

            return navigationItems;
        }
    }
}