using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Picums.Mvc.Navigation
{
	[DebuggerDisplay("Section:{Section},Name:{Name},Action:{Action},Controller:{Controller},IsAnonymous:{IsVisibleToAnonymous},IsVisibleToLoggedUsers:{IsVisibleToLoggedUsers},OrderNumber:{OrderNumber}")]
	public sealed class NavigationPrototype
	{
		public NavigationPrototype(
			IMenuAttribute menuAttribute
			, ActionModel action
			, bool isAnonymous
			, bool excludeWhenUserIsNonAnonymous)
		{
			this.IsVisibleToAnonymous = isAnonymous;
			this.IsVisibleToLoggedUsers = excludeWhenUserIsNonAnonymous;
			this.Action = action.ActionName;
			this.Controller = action.Controller.ControllerName;
			this.Section = menuAttribute.Section;
			this.Name = menuAttribute.Name;
			this.OrderNumber = menuAttribute.OrderNumber;
			this.Level = menuAttribute.Level;
		}

		public string Action { get; }

		public string Controller { get; }

		public bool IsVisibleToAnonymous { get; }

		public bool IsVisibleToLoggedUsers { get; }

		public string Section { get; }

		public string Name { get; }

		public int OrderNumber { get; }

		public int Level { get; }
	}
}