using System.Collections.Generic;
using System.Security.Claims;

namespace TheLizzards.Data.Types.Contracts
{
	public interface IClaimsProvider
	{
		IEnumerable<Claim> Claims { get; }
	}
}