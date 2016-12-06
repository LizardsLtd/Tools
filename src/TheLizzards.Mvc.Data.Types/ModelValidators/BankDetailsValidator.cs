using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TheLizzards.DataParts.Entites;
using TheLizzards.DataParts.Services;

namespace TheLizzards.Mvc.Data.Types.ModelValidators
{
	public sealed class BankDetailsModelValidator : IModelValidator
	{
		private readonly BankDetailsValidatorProvider validationProvider;

		public BankDetailsModelValidator(BankDetailsValidatorProvider validationProvider)
		{
			this.validationProvider = validationProvider;
		}

		public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
			=> validationProvider
				.GetProvider(CultureInfo.CurrentCulture)
				.Validate(context.Model as BankDetails)
				.ErrorMessages
				.Select(message
					=> new ModelValidationResult(
						context.ModelMetadata.PropertyName
						, message));
	}
}