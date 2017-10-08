using System;
using System.Collections.Generic;

namespace Picums.Mvc.Authorization
{
    public sealed class RequirePermissionAttribute : Attribute
    {
        public RequirePermissionAttribute(params string[] permissions)
        {
            this.Permissions = permissions;
        }

        public IEnumerable<string> Permissions { get; }
    }
}