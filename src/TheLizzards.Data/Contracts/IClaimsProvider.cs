using System.Collections.Generic;
using System.Security.Claims;

namespace TheLizzards.DataParts.Contracts
{
	public interface IClaimsProvider
	{
		IEnumerable<Claim> Claims { get; }
	}
}