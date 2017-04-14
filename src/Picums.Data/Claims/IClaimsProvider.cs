using System.Collections.Generic;
using System.Security.Claims;

namespace Picums.Data.Claims
{
	public interface IClaimsProvider
	{
		IEnumerable<Claim> Claims { get; }
	}
}