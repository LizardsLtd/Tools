using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Picums.Data.Types;
using Picums.Data.Types.Services;

namespace Picums.Mvc.ModelValidators
{
	public sealed class BankDetailsModelValidatorProvider : IModelValidatorProvider
	{
		private readonly BankDetailsValidatorProvider validator;

		public BankDetailsModelValidatorProvider()
		{
			this.validator = new BankDetailsValidatorProvider();
		}

		public void CreateValidators(ModelValidatorProviderContext context)
		{
			if (context.ModelMetadata.ModelType == typeof(BankDetails))
			{
				for (var i = 0; i < context.Results.Count; i++)
				{
					var validatorItem = context.Results[i];
					if (validatorItem.Validator != null)
					{
						continue;
					}

					var attribute = validatorItem.ValidatorMetadata as ValidationAttribute;
					if (attribute == null)
					{
						continue;
					}

					var validator = new BankDetailsModelValidator(this.validator);

					validatorItem.Validator = validator;
					validatorItem.IsReusable = true;

					if (attribute is RequiredAttribute)
					{
						context.Results.Remove(validatorItem);
						context.Results.Insert(0, validatorItem);
					}
				}
			}
		}
	}
}