using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheLizzards.Data.Types.Services
{
	public interface IBankDetailsValidator
	{
		IEnumerable<ValidationResult> Validate(BankDetails bankDetails);
	}
}