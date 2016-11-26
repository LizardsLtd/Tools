using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;
using TheLizzards.Mvc.ModelBinder;
using TheLizzards.Mvc.Navigation;
using TheLizzards.Mvc.Stratup;

namespace TheLizzards.Mvc.Startup
{
	public sealed class MvcConfiguration : ConfigurationBase
	{
		private readonly List<Action<MvcOptions>> mvcOptionsActions;
		private readonly List<Action<IMvcBuilder>> mvcBuilderActions;
		private readonly RouteConfigurator routeBuilder;

		public MvcConfiguration(IConfiguration startup)

			: base(startup)
		{
			this.mvcOptionsActions = new List<Action<MvcOptions>>(15);
			this.mvcBuilderActions = new List<Action<IMvcBuilder>>(15);
			this.routeBuilder = new RouteConfigurator();
		}

		public MvcConfiguration AddMvcOption(Action<MvcOptions> action)
		{
			this.mvcOptionsActions.Add(action);
			return this;
		}

		public MvcConfiguration AddMvcBuilderAction(Action<IMvcBuilder> action)
		{
			this.mvcBuilderActions.Add(action);
			return this;
		}

		public MvcConfiguration AddMvcFilter<TFilterMetadata>()
			where TFilterMetadata : IFilterMetadata, new()
			=> AddMvcFilter(new TFilterMetadata());

		public MvcConfiguration AddMvcFilter(IFilterMetadata filter)
		{
			this.mvcOptionsActions.Add(options => options.Filters.Add(filter));
			return this;
		}

		public MvcConfiguration AddModelBinderProvider<TBindedType, TModelBinder>()
			where TModelBinder : IModelBinder, new()
			=> AddModelBinderProvider(new ModelBinderProvider<TBindedType, TModelBinder>());

		public MvcConfiguration AddModelBinderProvider(IModelBinderProvider provider)
		{
			this.mvcOptionsActions.Add(options => options.ModelBinderProviders.Add(provider));
			return this;
		}

		public MvcConfiguration AddModelValidator<TModelValidatorProvider>()
			where TModelValidatorProvider : IModelValidatorProvider, new()
			=> AddModelValidator(new TModelValidatorProvider());

		public MvcConfiguration AddModelValidator(IModelValidatorProvider validatorFactory)
		{
			this.mvcOptionsActions.Add(options => options.ModelValidatorProviders.Add(validatorFactory));
			return this;
		}

		public MvcConfiguration AddApplicationConvention<T>()
			where T : IApplicationModelConvention, new()
			=> AddConvention(new T());

		public MvcConfiguration AddConvention(IApplicationModelConvention convention)
		{
			this.mvcOptionsActions.Add(options => options.Conventions.Add(convention));
			return this;
		}

		public MvcConfiguration AddControllerConvention<T>()
			where T : IControllerModelConvention, new()
			=> AddConvention(new T());

		public MvcConfiguration AddConvention(IControllerModelConvention convention)
		{
			this.mvcOptionsActions.Add(options => options.Conventions.Add(convention));
			return this;
		}

		public MvcConfiguration AddRoute(string name, string controller, string action, string parameters = "")
		{
			this.routeBuilder.AddRoute(name, controller, action, parameters);
			return this;
		}

		internal void SetupMvcService(IServiceCollection services)
		{
			var mvcBuilder = services.AddMvc();

			this.mvcBuilderActions.ForEach(x => x(mvcBuilder));

			services
				.AddSingleton(AddNavigationItems(services, mvcBuilder))
				.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

			this.mvcOptionsActions.ForEach(x => mvcBuilder.AddMvcOptions(x));
		}

		internal void SetupUseMvc(IApplicationBuilder app)
		{
			app.UseMvc(this.routeBuilder.BuildRoutes);
		}

		private NavigationItems AddNavigationItems(IServiceCollection services, IMvcBuilder mvcBuilder)
		{
			var navigationItems = new NavigationItems();

			this.AddConvention(new NavigationCreationConvention(navigationItems));

			return navigationItems;
		}
	}
}