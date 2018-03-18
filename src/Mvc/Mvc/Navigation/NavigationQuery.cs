using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Picums.Web.Shared;
using INavigationQueryWithUrlHelper =
	Picums.Mvc.Navigation.IRequireUrlHelper<Picums.Mvc.Navigation.INavigationItemQuery>;

namespace Picums.Mvc.Navigation
{
	public sealed class NavigationQuery :
		IRequireTranslator<INavigationQueryWithUrlHelper>
		, INavigationQueryWithUrlHelper
		, INavigationItemQuery
	{
		private IEnumerable<NavigationPrototype> items;
		private IStringLocalizer translator;
		private IUrlHelper urlHelper;

		internal NavigationQuery(IEnumerable<NavigationPrototype> items)
		{
			this.items = items;
		}

		public INavigationQueryWithUrlHelper WithTranslator(IStringLocalizer translator)
		{
			this.translator = translator;
			return this;
		}

		public INavigationItemQuery WithUrlHelper(IUrlHelper helper)
		{
			this.urlHelper = helper;
			return this;
		}

		public INavigationItemQuery FilterBasedOnUserAuthenticationStatus(bool userIsAuthenticated)
		{
			this.items = userIsAuthenticated
				? this.items.Where(x => x.IsVisibleToLoggedUsers)
				: this.items.Where(x => x.IsVisibleToAnonymous);

			return this;
		}

		public INavigationItemQuery FilterByLevels(IEnumerable<int> levels)
		{
			this.items = this.items.Where(x => levels.Contains(x.Level));

			return this;
		}

		public INavigationItemQuery IncludeItemsBySections(IEnumerable<string> sections)
			=> IncludeItemsBySections(sections.ToArray());

		public IEnumerable<NavigationItem> Build()
			=> this.items
				.Select(x => new NavigationItem(
					GetName(x),
					GetUrl(x)))
				.ToArray();

		private INavigationItemQuery IncludeItemsBySections(string[] sections)
		{
			var index = 1;
			var sortedSections = sections.Select(x => new
			{
				Index = index++,
				Section = x
			}).ToArray();

			this.items = items
				.Where(x => sections.Contains(x.Section))
				.Select(x => new
				{
					SectionIndex = sortedSections.Single(y => y.Section == x.Section).Index,
					Item = x
				})
				.ToArray()
				.Select(x => new
				{
					Index = $"{x.SectionIndex}{x.Item.OrderNumber}",
					Item = x.Item
				})
				.OrderBy(x => x.Index)
				.Select(x => x.Item);

			return this;
		}

		private string GetUrl(NavigationPrototype navigationItem)
			=> this.urlHelper.Action(
				navigationItem.Action
				, navigationItem.Controller);

		private string GetName(NavigationPrototype item)
			=> this.translator.GetString($"Navigation.{item.Section}.{item.Name}");
	}
}