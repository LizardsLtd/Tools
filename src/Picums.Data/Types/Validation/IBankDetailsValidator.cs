using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Picums.Data.Types.Services
{
	public interface IBankDetailsValidator
	{
		IEnumerable<ValidationResult> Validate(BankDetails bankDetails);
	}
}