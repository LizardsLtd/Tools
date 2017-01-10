using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheLizzards.Localisation.Entities;

namespace TheLizzards.Localisation.Services
{
	public sealed class GoogleGeocodingService : IGeocodingService
	{
		private const string serviceUrl = "http://maps.google.com/maps/api/geocode/xml?address={address}&sensor=false";

		public GoogleGeocodingService()
		{
		}

		public async Task<LocationPoint> GeocodeAsync(Address address)
		{
			var encodedAddress = GetEncodedAddress(address);
			var formattedQueryUrl =
				serviceUrl
				.Replace("{address}", encodedAddress);

			var client = new HttpClient();

			var result = await client.GetAsync(formattedQueryUrl);

			var resultContent = await result.Content.ReadAsStringAsync();

			var location = XDocument
				.Parse(resultContent)
				.Document
				.Descendants(XName.Get("location"))
				.First();
			var latitude = double.Parse(location
				.Descendants(XName.Get("lat"))
				.First()
				.Value);
			var longitude = double.Parse(location
				.Descendants(XName.Get("lng"))
				.First()
				.Value);

			return new LocationPoint(latitude, longitude);
		}

		private string GetEncodedAddress(Address address)
			=> WebUtility.HtmlEncode(address.ToString());
	}
}
