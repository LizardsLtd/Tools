using System.Collections.Generic;
using System.Security.Claims;

namespace Picums.Mvc.UserAccess.Claims
{
    public interface IClaimsProvider
    {
        IEnumerable<Claim> Claims { get; }
    }
}