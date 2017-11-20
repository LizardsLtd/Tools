using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Picums.Mvc.UserAccess.Claims
{
    public sealed class PermissionClaimProvider : IClaimProvider
    {
        public IEnumerable<Claim> GetClaims(IUser user)
            => user
                .Permissions
                .Select(permission => new Claim(ClaimIdentity.Permission, permission.Id.ToString()));
    }
}