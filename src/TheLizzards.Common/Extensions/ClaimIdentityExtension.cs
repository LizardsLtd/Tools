using System.Security.Claims;

namespace TheLizzards.Common.Extensions
{
	public static class ClaimIdentityExtension
	{
		public static ClaimsIdentity AddChainableClaim(this ClaimsIdentity identity, Claim claim)
		{
			identity.AddClaim(claim);

			return identity;
		}
	}
}