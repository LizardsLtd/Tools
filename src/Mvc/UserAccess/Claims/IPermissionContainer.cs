using System;
using System.Collections.Generic;
using Picums.Data.Domain;

namespace Picums.Mvc.UserAccess.Claims
{
    public interface IPermissionContainer
    {
        IEnumerable<Permission> Permissions { get; }
    }
}