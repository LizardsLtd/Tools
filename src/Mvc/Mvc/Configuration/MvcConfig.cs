using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Picums.Mvc.Navigation;

namespace Picums.Mvc.Configuration
{
    public sealed class MvcConfig
    {
        public MvcConfig()
        {
            Options = new Configurator<MvcOptions>();
            Routes = new Configurator<IRouteBuilder>();
        }

        public Configurator<MvcOptions> Options { get; }

        public Configurator<IRouteBuilder> Routes { get; }

        internal void AddMvc(IServiceCollection services)
        {
            var mvcBuilder = services
                .AddMvc(CreateMvcOptions)
                .AddViewLocalization();

            services.AddSingleton(AddNavigationItems(services, mvcBuilder));
        }

        internal void Use(IApplicationBuilder app)
        {
            app.UseMvc(routes => this.Routes.Execute(routes));
        }

        private void CreateMvcOptions(MvcOptions options)
            => this.Options.Execute(options);

        private NavigationItems AddNavigationItems(IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            var navigationItems = new NavigationItems();

            this.Options.AddApplicationConvention(new NavigationCreationConvention(navigationItems));
            return navigationItems;
        }
    }
}