using System;
using System.Collections.Generic;
using System.Globalization;
using TheLizzards.Data.Types.Services.Poland;

namespace TheLizzards.Data.Types.Services
{
	public sealed class BankDetailsValidatorProvider
	{
		private readonly Dictionary<string, Lazy<IBankDetailsValidator>> providers;

		public BankDetailsValidatorProvider()
		{
			this.providers = new Dictionary<string, Lazy<IBankDetailsValidator>>();
			this.AddPolishValidators();
		}

		public IBankDetailsValidator GetProvider(CultureInfo culture)
			=> this.providers[culture.TwoLetterISOLanguageName].Value;

		private void AddPolishValidators()
			=> this.providers[new CultureInfo("pl-PL").TwoLetterISOLanguageName]
				= new Lazy<IBankDetailsValidator>(()
					=> new PolishBankDetailsValidator());
	}
}