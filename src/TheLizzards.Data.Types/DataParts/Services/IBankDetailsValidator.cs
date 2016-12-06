using TheLizzards.Common.Data;
using TheLizzards.DataParts.Entites;

namespace TheLizzards.DataParts.Services
{
	public interface IBankDetailsValidator
	{
		IResult Validate(BankDetails bankDetails);
	}
}