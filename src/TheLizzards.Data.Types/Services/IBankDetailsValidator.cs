using System.ComponentModel.DataAnnotations;
using TheLizzards.Data.Types.Entites;

namespace TheLizzards.Data.Types.Services
{
	public interface IBankDetailsValidator
	{
		ValidationResult Validate(BankDetails bankDetails);
	}
}