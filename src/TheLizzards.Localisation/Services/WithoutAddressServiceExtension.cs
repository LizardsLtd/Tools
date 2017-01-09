using System;
using System.Collections.Generic;
using System.Text;
using TheLizzards.Localisation.Entities;

namespace TheLizzards.Localisation.Services
{
	public static class WithoutAddressServiceExtension
	{
		public static LocationPoint Convert(
			this ILocalisationService locationService
			, string houseNumber = ""
			, string streetName = ""
			, string district = ""
			, string city = ""
			, string province = ""
			, string country = ""
			, string postCode = "")
		{
			var address = new Address(houseNumber, streetName, district, city, province, country, postCode);

			return locationService.Convert(address);
		}
	}
}
