using System.Collections.Generic;
using System.Security.Claims;

namespace Picums.Mvc.UserAccess.Claims
{
    public interface IClaimProvider
    {
        IEnumerable<Claim> GetClaims(IUser user);
    }
}