using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Picums.Mvc.FeatureSlices
{
	internal sealed class FeatureConvention : IControllerModelConvention

	{
		public void Apply(ControllerModel controller)
		{
			controller.Properties.Add(
				"feature"
				, GetFeatureName(controller.ControllerType));

			var alternativeGroupingName = GetGroupView(controller.ControllerType);
			if (!string.IsNullOrEmpty(alternativeGroupingName))
			{
				controller.Properties.Add(
					"feature-group"
					, alternativeGroupingName);
			}
		}

		private string GetGroupView(TypeInfo controllerType)
		{
			var alternativeViewGroupingName = controllerType.GetCustomAttribute<GroupedLocationAttribute>();

			return alternativeViewGroupingName?.GrupeName ?? string.Empty;
		}

		private string GetFeatureName(TypeInfo controllerType)
		{
			var tokens = controllerType.FullName.Split('.');
			if (!tokens.Any(t => t == "Features"))
			{
				return "";
			}
			string featureName = tokens
			  .SkipWhile(t => !t.Equals("features", StringComparison.CurrentCultureIgnoreCase))
			  .Skip(1)
			  .Take(1)
			  .FirstOrDefault();

			return featureName;
		}
	}
}