using CSharpVerbalExpressions;
using System.ComponentModel.DataAnnotations;
using TheLizzards.Data.Types.Entites;

namespace TheLizzards.Data.Types.Services.Poland {

	public sealed class PolishBankDetailsValidator : IBankDetailsValidator {

		public ValidationResult Validate(BankDetails bankDetails)
			=> IsValidAccountNumber(bankDetails.AccountNumber.ToString());

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