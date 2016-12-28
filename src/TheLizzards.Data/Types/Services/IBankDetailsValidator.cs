using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheLizzards.Data.Types.Entites;

namespace TheLizzards.Data.Types.Services
{
	public interface IBankDetailsValidator
	{
		IEnumerable<ValidationResult> Validate(BankDetails bankDetails);
	}
}