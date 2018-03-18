using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Picums.Data.Types;
using Picums.Data.Types.Services;

namespace Picums.Mvc.ModelValidators
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