using System;
using System.Collections.Generic;

namespace Picums.Mvc.Claims.Entities
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