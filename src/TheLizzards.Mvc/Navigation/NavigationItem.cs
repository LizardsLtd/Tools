using System.Diagnostics;

namespace Picums.Web.Shared
{
	[DebuggerDisplay("Name:{Name}; URL:{TargetUrl}")]
	public sealed class NavigationItem
	{
		public NavigationItem(string name, string targetUrl)
		{
			Name = name;
			TargetUrl = targetUrl;
		}

		public string Name { get; }

		public string TargetUrl { get; }
	}
}