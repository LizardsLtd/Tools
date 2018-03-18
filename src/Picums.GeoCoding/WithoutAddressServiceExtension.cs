using System.Threading.Tasks;
using Microsoft.Spatial;
using Picums.Maybe;

namespace Picums.GeoCoding
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