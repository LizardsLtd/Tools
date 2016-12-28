using System.Collections.Generic;
using CSharpVerbalExpressions;
using System.ComponentModel.DataAnnotations;
using TheLizzards.Data.Types.Entites;

namespace TheLizzards.Data.Types.Services.Poland {

	public sealed class PolishBankDetailsValidator : IBankDetailsValidator {

		public IEnumerable<ValidationResult> Validate(BankDetails bankDetails)
			=> new[] {
				IsValidAccountNumber(bankDetails.AccountNumber.ToString())
			};

		private ValidationResult IsValidAccountNumber(string accountNumber)
			=> new VerbalExpressions()
				.StartOfLine()
				.Any("0-9")
				.RepeatPrevious(26)
				.EndOfLine()
				.Test(accountNumber)
				.ToValidationResult("Error.Account.InvalidNumber");
	}
}