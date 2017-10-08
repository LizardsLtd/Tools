using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Picums.Mvc
{
	public static class CultureInfoExtension
	{
		public static IList<CultureInfo> ToCulturesList(this IConfigurationSection section)
			=> section
				.GetChildren()
				.Select(x => x.Value)
				.Select(x => new CultureInfo(x))
				.ToList();
	}
}