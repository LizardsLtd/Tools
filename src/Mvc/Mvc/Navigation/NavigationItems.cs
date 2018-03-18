using System.Collections.Generic;
using System.Linq;
using INavigationQueryWithUrlHelper =
	Picums.Mvc.Navigation.IRequireUrlHelper<Picums.Mvc.Navigation.INavigationItemQuery>;

namespace Picums.Mvc.Navigation
{
	public sealed class NavigationItems
	{
		private List<NavigationPrototype> navigationItems;

		public NavigationItems()
		{
		}

		public void AddRange(IEnumerable<NavigationPrototype> navigationItems)
		{
			this.navigationItems = navigationItems.ToList();
		}

		public IRequireTranslator<INavigationQueryWithUrlHelper> GetQuery()
			=> new NavigationQuery(this.navigationItems);
	}
}