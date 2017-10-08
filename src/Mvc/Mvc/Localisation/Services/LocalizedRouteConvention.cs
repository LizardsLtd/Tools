using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Internal;
using Picums.Mvc.Localisation;
using AttributeConstruct = System.Tuple<
	Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel,
	Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerModel,
	Picums.Mvc.Localisation.LocalizedRouteAttribute>;

namespace Picums.Mvc.Localisation.Services
{
	public sealed class LocalizedRouteConvention : IApplicationModelConvention
	{
		public readonly Dictionary<string, LocalizedRouteInformation[]> localizedRoutes;

		public LocalizedRouteConvention
			(Dictionary<string, LocalizedRouteInformation[]> localizedRoutes)
		{
			this.localizedRoutes = localizedRoutes;
		}

		public void Apply(ApplicationModel application)
		{
			application
				.Controllers
				.SelectMany(controller => controller.Actions)
				.SelectMany(SeparateActionModels)
				.ToList()
				.ForEach(x =>
				{
					x.Item2.Actions.Remove(x.Item1);
					GetLocalizedVersionsForARoute(x.Item3.Name)
						.ToList()
						.ForEach(localisedRoute =>
						{
							var actionModel = new ActionModel(x.Item1);
							var selector = new SelectorModel
							{
								AttributeRouteModel = localisedRoute
							};
							selector.ActionConstraints.Add(
								x.Item1.Attributes.Any(attr => attr.GetType() == typeof(HttpGetAttribute))
									? new HttpMethodActionConstraint(new[] { "get" })
									: new HttpMethodActionConstraint(new[] { "post" }));

							actionModel.Selectors.Add(selector);
							x.Item2.Actions.Add(actionModel);
						});
				});

			//actions
			//	.ExecuteAction(action => action.Controller.Actions.Remove(action));

			//.SelectMany(x => new
			//{
			//	Controller = x.Controller,
			//	OldAction = x.Action,
			//	NewActions = x.Attributes
			//		.Select(attribute =>
			//		{
			//			var newAction = new ActionModel(x.Action);
			//			var selector = new SelectorModel
			//			{
			//				AttributeRouteModel = GetLocalizedVersionsForARoute(attribute.Name)
			//			};
			//			if (x.Action.Attributes.Any(attr => attr.GetType() == typeof(HttpGetAttribute)))
			//			{
			//				selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { "get" }));
			//			}
			//			else
			//			{
			//				selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { "post" }));
			//			}

			//			newAction.Selectors.Add(selector);
			//			return newAction;
			//		})
			//});
		}

		private IEnumerable<AttributeConstruct> SeparateActionModels(ActionModel action)
		{
			var attributes = action
				.Attributes
				.OfType<LocalizedRouteAttribute>();

			foreach (var attribute in attributes)
			{
				yield return Tuple.Create(
					action,
					action.Controller,
					attribute);
			}
		}

		private IEnumerable<AttributeRouteModel> GetLocalizedVersionsForARoute(string name)
			=> localizedRoutes.ContainsKey(name)
				? ConvertLocalisedRouteToAttributeRouteModel(name)
				: new AttributeRouteModel[0];

		private IEnumerable<AttributeRouteModel> ConvertLocalisedRouteToAttributeRouteModel(string name)
			=> localizedRoutes[name]
				.Select(entry =>
					new AttributeRouteModel(
						new RouteAttribute(entry.GetTempalteWithChangeCulture())
						{
							Name = name + entry.Culture
						}));

		//private IEnumerable<ActionModel> ProcessActions(ActionModel action)
		//	=> action
		//		.Attributes
		//		.OfType<LocalizedRouteAttribute>()
		//		.SelectMany(attribute => GetLocalizedVersionsForARoute(attribute.Name))
		//		.Select(localizedVersion =>
		//		{
		//			var actionModel = new ActionModel(action);
		//			var selector = new SelectorModel
		//			{
		//				AttributeRouteModel = localizedVersion
		//			};
		//			if (action.Attributes.Any(attr => attr.GetType() == typeof(HttpGetAttribute)))
		//			{
		//				selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { "get" }));
		//			}
		//			else
		//			{
		//				selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { "post" }));
		//			}

		//			actionModel.Selectors.Add(selector);
		//		});
	}
}