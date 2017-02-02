using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheLizzards.Localisation.Entities;
using TheLizzards.Maybe;
using System.Spatial;

namespace TheLizzards.Localisation.Services
{
	public static class WithoutAddressServiceExtension
	{
		public static async Task<Maybe<GeographyPoint>> GeocodeAsync(
			this IGeocodingService locationService
			, string houseNumber = ""
			, string streetName = ""
			, string district = ""
			, string city = ""
			, string province = ""
			, string country = ""
			, string postCode = "")
		{
			var address = new Address(houseNumber, streetName, district, city, province, country, postCode);

			return await locationService.GeocodeAsync(address);
		}
	}
}
