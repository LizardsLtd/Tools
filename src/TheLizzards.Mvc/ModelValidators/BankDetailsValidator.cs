using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TheLizzards.Data.Types;
using TheLizzards.Data.Types.Services;

namespace TheLizzards.Mvc.ModelValidators
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
				.SelectMany(validationError
					=> validationError
						.MemberNames
						.Select(member
							=> new
							{
								Member = member,
								Message = validationError.ErrorMessage
							})
				)
				.Select(message
					=> new ModelValidationResult(
							message.Member
							, message.Message));
	}
}