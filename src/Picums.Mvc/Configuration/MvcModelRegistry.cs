using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using Picums.Mvc.ModelBinder;

namespace Picums.Mvc.Configuration
{
    public sealed class MvcModelRegistry
    {
        private readonly List<Action<Microsoft.AspNetCore.Mvc.MvcOptions>> models;

        internal MvcModelRegistry()
        {
            models = new List<Action<Microsoft.AspNetCore.Mvc.MvcOptions>>(25);
        }

        public MvcModelRegistry AddModelBinderProvider<TBindedType, TModelBinder>()
                where TModelBinder : IModelBinder, new()
            => AddModelBinderProvider(new ModelBinderProvider<TBindedType, TModelBinder>());

        public MvcModelRegistry AddModelBinderProvider(IModelBinderProvider provider)
        {
            this.models.Add(options => options.ModelBinderProviders.Add(provider));
            return this;
        }

        public MvcModelRegistry AddModelValidator<TModelValidatorProvider>()
                where TModelValidatorProvider : IModelValidatorProvider, new()
            => AddModelValidator(new TModelValidatorProvider());

        public MvcModelRegistry AddModelValidator(IModelValidatorProvider validatorFactory)
        {
            this.models.Add(options => options.ModelValidatorProviders.Add(validatorFactory));
            return this;
        }

        internal void Execute(Microsoft.AspNetCore.Mvc.MvcOptions options) => models.ForEach(x => x(options));
    }
}