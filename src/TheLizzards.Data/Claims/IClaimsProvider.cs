using System.Collections.Generic;
using System.Security.Claims;

namespace TheLizzards.Data.Claims
{
	public interface IClaimsProvider
	{
		IEnumerable<Claim> Claims { get; }
	}
}