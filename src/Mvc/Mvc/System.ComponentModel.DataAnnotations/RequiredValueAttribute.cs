using Microsoft.Extensions.Localization;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class RequiredValueAttribute : ValidationAttribute
	{
		private object valueToCompare;

		public RequiredValueAttribute(object valueToCompare)
			: this(valueToCompare, "The value for field {0} does not match required one.")
		{
		}

		public RequiredValueAttribute(object valueToCompare, Func<string> errorMessageAccessor)
			: base(errorMessageAccessor)
		{
			this.valueToCompare = valueToCompare;
		}

		public RequiredValueAttribute(object valueToCompare, string errorMessage)
			: base(errorMessage)
		{
			this.valueToCompare = valueToCompare;
		}

		public override bool IsValid(object value)
			=> valueToCompare?.Equals(value) ?? false;

		public void TranslatErrorMessage(IStringLocalizer translator)
			=> this.ErrorMessage = translator.GetString(this.ErrorMessageString);
	}
}