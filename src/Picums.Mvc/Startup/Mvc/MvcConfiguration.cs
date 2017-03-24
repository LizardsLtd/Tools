using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using TheLizzards.Data.Types;
using TheLizzards.Mvc.ApplicationServices;
using TheLizzards.Mvc.Claims.Entities;
using TheLizzards.Mvc.FeatureSlices;
using TheLizzards.Mvc.Localisation.Services;
using TheLizzards.Mvc.ModelValidators;

namespace TheLizzards.Mvc.Startup
{
    public sealed class MvcConfiguration
    {
        private readonly List<Action<MvcOptions>> mvcOptionsActions;
        private readonly RouteConfigurator routeBuilder;
        private readonly IStringLocalizer localiser;

        public MvcConfiguration(IStringLocalizer localiser)
        {
            this.mvcOptionsActions = new List<Action<MvcOptions>>(15);
            this.routeBuilder = new RouteConfigurator();
            this.localiser = localiser;
        }

        public MvcConfiguration AddDataParts()
            => this
                .AddBankModelHandlers()
                .AddEmailModelHandlers()
                .AddAddressModelHandlers();

        public MvcConfiguration AddBankModelHandlers()
            => this.AddModelBinderProvider<BankDetails, BankDetailsModelBinder>()
                    .AddModelValidator<BankDetailsModelValidatorProvider>();

        public MvcConfiguration AddEmailModelHandlers()
            => this.AddModelBinderProvider<Email, EmailModelBinder>();

        public MvcConfiguration AddAddressModelHandlers()
            => this.AddModelBinderProvider<Address, AddressModelBinder>();

        //public MvcConfiguration AddApplicationConvention<T>()
        //        where T : IApplicationModelConvention, new()
        //    => AddConvention(new T());

        //public MvcConfiguration AddConvention(IApplicationModelConvention convention)
        //{
        //    this.mvcOptionsActions.Add(options => options.Conventions.Add(convention));
        //    return this;
        //}

        //public MvcConfiguration AddControllerConvention<T>() where T : IControllerModelConvention, new()
        //    => AddConvention(new T());

        //public MvcConfiguration AddConvention(IControllerModelConvention convention)
        //{
        //    this.mvcOptionsActions.Add(options => options.Conventions.Add(convention));
        //    return this;
        //}

        public MvcConfiguration AddRoute(string name, string controller, string action, string parameters = "")
        {
            this.routeBuilder.AddRoute(name, controller, action, parameters);
            return this;
        }

        public MvcConfiguration AddAllTranslators()
        {
            return this
                   .AddDisplayAttributeProvider()
                   .AddValidationAttributeProvider();
        }

        public MvcConfiguration AddDisplayAttributeProvider()
        {
            this.mvcOptionsActions.Add(options
                => options.ModelMetadataDetailsProviders.Add(new DisplayAttributeLocalisationProvider(this.localiser)));
            return this;
        }

        public MvcConfiguration AddValidationAttributeProvider()
        {
            this.mvcOptionsActions.Add(options
                => options.ModelMetadataDetailsProviders.Add(new ValidationAttributeLocalisationProvider(this.localiser)));
            return this;
        }

        public MvcConfiguration ActivateFeatures()
        {
            this.AddControllerConvention<FeatureConvention>();

            return this;
        }

        public MvcConfiguration AddPermissionBasedAuthorization()
            => this.AddMvcFilter(
                    new AuthorizeFilter(
                        new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .AddRequirements(new PermissionRequirement())
                            .Build()));

        internal void SetupUseMvc(IApplicationBuilder app)
        {
            app.UseMvc(this.routeBuilder.BuildRoutes);
        }
    }
}