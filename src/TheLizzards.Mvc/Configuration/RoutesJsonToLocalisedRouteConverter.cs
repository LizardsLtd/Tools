using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using TheLizzards.Mvc.Localisation.Entities;

namespace TheLizzards.Mvc.Configuration
{
	public static class RoutesJsonToLocalisedRouteConverter
	{
		public static Dictionary<string, LocalizedRouteInformation[]> CreateConfiguration(
			IConfigurationRoot root)
			=> root
				.GetSection("Routes")
				.GetChildren()
				.ToDictionary(
					key => key.Key,
					value => value
						.GetChildren()
						.Select(configSection =>
							new LocalizedRouteInformation(
								configSection["culture"],
								configSection["template"]))
						.ToArray());
	}
}