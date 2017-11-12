using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Picums.Mvc.ModelBinder;

namespace Picums.Mvc.Configuration
{
    public static class ModelBinderExtension
    {
        public static Configurator<MvcOptions> AddModelBinderProvider<TBindedType, TModelBinder>(
                this Configurator<MvcOptions> options)
                where TModelBinder : IModelBinder, new()
            => options.AddModelBinderProvider(new ModelBinderProvider<TBindedType, TModelBinder>());

        public static Configurator<MvcOptions> AddModelBinderProvider(
                this Configurator<MvcOptions> options,
                IModelBinderProvider provider)
            => options.Add(option => option.ModelBinderProviders.Insert(0, provider));

        public static Configurator<MvcOptions> AddModelValidator<TModelValidatorProvider>(
                this Configurator<MvcOptions> options)
                where TModelValidatorProvider : IModelValidatorProvider, new()
            => options.AddModelValidator(new TModelValidatorProvider());

        public static Configurator<MvcOptions> AddModelValidator(
                this Configurator<MvcOptions> options,
                IModelValidatorProvider validatorFactory)
            => options.Add(option => option.ModelValidatorProviders.Insert(0, validatorFactory));
    }
}