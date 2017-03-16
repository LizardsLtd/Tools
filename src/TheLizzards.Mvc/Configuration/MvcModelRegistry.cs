using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using TheLizzards.Mvc.ModelBinder;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class MvcModelRegistry
    {
        private readonly List<Action<MvcOptions>> models;

        internal MvcModelRegistry()
        {
            models = new List<Action<MvcOptions>>(25);
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

        internal void Execute(MvcOptions options) => models.ForEach(x => x(options));
    }
}