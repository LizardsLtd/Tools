using System;
using System.Collections.Generic;

namespace TheLizzards.Mvc.Claims.Entities
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