using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Picums.Mvc.FeatureSlices
{
	internal sealed class FeatureViewLocationExpander : IViewLocationExpander
	{
		public IEnumerable<string> ExpandViewLocations(
			ViewLocationExpanderContext context
			, IEnumerable<string> viewLocations)
		{
			// Error checking removed for brevity
			var controllerActionDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
			var featureName = controllerActionDescriptor.Properties["feature"] as string;
			var featureGroupName = GetProperty(controllerActionDescriptor, "feature-group");

			foreach (var location in viewLocations)
			{
				yield return location.Replace("{3}", featureName);

				if (!string.IsNullOrEmpty(featureGroupName))
				{
					yield return location
						.Replace("{3}", featureName)
						.Replace("{1}", featureGroupName);
				}
			}
		}

		public void PopulateValues(ViewLocationExpanderContext context)
		{
		}

		private static string GetProperty(
			ControllerActionDescriptor controllerActionDescriptor
			, object key)
		{
			return controllerActionDescriptor.Properties.ContainsKey(key)
				? controllerActionDescriptor.Properties[key] as string
				: string.Empty;
		}
	}
}