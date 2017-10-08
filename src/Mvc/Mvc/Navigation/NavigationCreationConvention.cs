using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Picums.Mvc.Navigation
{
	public sealed class NavigationCreationConvention : IApplicationModelConvention
	{
		private readonly NavigationItems navigationItems;

		public NavigationCreationConvention(NavigationItems navigationItems)
		{
			this.navigationItems = navigationItems;
		}

		public void Apply(ApplicationModel application)
			=> navigationItems.AddRange(ConvertActionInfoIntoNavigationItem(application));

		private static bool IsVisibleToAnonymous(ControllerModel controller)
			=> controller.Attributes.OfType<AllowAnonymousAttribute>().Any();

		private static bool IsVisibleToLoggedUsers(ControllerModel controller)
			=> !controller.Attributes.OfType<AnonumousOnlyAttribute>().Any();

		private List<NavigationPrototype> ConvertActionInfoIntoNavigationItem(ApplicationModel application)
							=> application
				.Controllers
				.Select(x => new
				{
					Controller = x,
					AllowAnonymous = IsVisibleToAnonymous(x),
					AnonymousOnly = IsVisibleToLoggedUsers(x),
				})
				.SelectMany(contoller =>
					contoller
						.Controller
						.Actions
						.Select(x
							=> Tuple.Create(
								x
								, contoller.AllowAnonymous
								, contoller.AnonymousOnly)))
				.Where(x => x.Item1.Attributes.OfType<MenuAttribute>().Any())
				.Select(x
					=> new NavigationPrototype(
						RetrieveMenuAttribute(x.Item1)
						, x.Item1
						, x.Item2 || IsVisibleToAnonymous(x.Item1)
						, x.Item3 && IsVisibleToLoggedUsers(x.Item1)))
				.ToList();

		private IMenuAttribute RetrieveMenuAttribute(ActionModel action)
			=> action
				.Attributes
				.OfType<MenuAttribute>()
				.SingleOrDefault();

		private bool IsVisibleToAnonymous(ActionModel model)
			=> model.Attributes.OfType<AllowAnonymousAttribute>().Any();

		private bool IsVisibleToLoggedUsers(ActionModel model)
			=> !model.Attributes.OfType<AnonumousOnlyAttribute>().Any();
	}
}