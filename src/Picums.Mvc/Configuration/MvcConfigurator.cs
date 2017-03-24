using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Picums.Mvc.Navigation;

namespace Picums.Mvc.Configuration
{
    public sealed class MvcConfigurator
    {
        public MvcConfigurator()
        {
            Conventions = new MvcConventionRegistry();
            Models = new MvcModelRegistry();
            Options = new MvcOptionsConfigurator();
            Filters = new FilterRegistry();
            Routes = new RouteConfigurator();
        }

        public MvcConventionRegistry Conventions { get; }

        public MvcModelRegistry Models { get; }

        public MvcOptionsConfigurator Options { get; }

        public FilterRegistry Filters { get; }

        public RouteConfigurator Routes { get; }

        internal void AddMvc(IServiceCollection services)
        {
            var mvcBuilder = services
                .AddMvc(CreateMvcOptions)
                .AddViewLocalization();

            services.AddSingleton(AddNavigationItems(services, mvcBuilder));
        }

        internal void Use(IApplicationBuilder app)
        {
            app.UseMvc(this.Routes.BuildRoutes);
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