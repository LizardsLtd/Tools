using CSharpVerbalExpressions;
using TheLizzards.Common.Data;
using TheLizzards.DataParts.Entites;

namespace TheLizzards.DataParts.Services.Poland
{
	public sealed class PolishBankDetailsValidator : IBankDetailsValidator
	{
		public IResult Validate(BankDetails bankDetails)
			=> IsValidAccountNumber(bankDetails.AccountNumber.ToString());

		private IResult IsValidAccountNumber(string accountNumber)
			=> new VerbalExpressions()
				.StartOfLine()
				.Any("0-9")
				.RepeatPrevious(26)
				.EndOfLine()
				.Test(accountNumber)
				.ToResult("Error.Account.InvalidNumber");
	}
}