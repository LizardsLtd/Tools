﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Localization;

namespace TheLizzards.Mvc.Localisation.Services
{
    public sealed class ValidationAttributeLocalisationProvider : IValidationMetadataProvider
    {
        private Lazy<IStringLocalizer> localiser;

        public ValidationAttributeLocalisationProvider(Lazy<IStringLocalizer> localiser)
        {
            this.localiser = localiser;
        }

        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
            => context
                .PropertyAttributes
                ?.Where(attribute => attribute is ValidationAttribute)
                .Cast<ValidationAttribute>()
                .ToList()
                .ForEach(x
                    => x.ErrorMessage = this.localiser.Value.GetString($"Errors.{x.GetType().Name}", GetName(context)));

        private string GetName(ValidationMetadataProviderContext context)
        {
            var display = context.PropertyAttributes.OfType<DisplayAttribute>().FirstOrDefault();

            return display != null
                ? display.Name
                : context.Key.Name;
        }
    }
}