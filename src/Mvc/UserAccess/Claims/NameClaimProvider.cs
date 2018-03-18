using System.Collections.Generic;
using System.Security.Claims;

namespace Picums.Mvc.UserAccess.Claims
{
    public sealed class NameClaimProvider : IClaimProvider
    {
        public IEnumerable<Claim> GetClaims(IUser user)
        {
            yield return new Claim(ClaimIdentity.UserName, user.UserName);
        }
    }
}