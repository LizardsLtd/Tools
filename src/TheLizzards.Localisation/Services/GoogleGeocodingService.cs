using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheLizzards.Localisation.Entities;
using TheLizzards.Maybe;
using Microsoft.Spatial;

namespace TheLizzards.Localisation.Services
{
	public sealed class GoogleGeocodingService : IGeocodingService
	{
		private const string serviceUrl = "http://maps.google.com/maps/api/geocode/xml?address={address}&sensor=false";

		public GoogleGeocodingService()
		{
		}

		public async Task<Maybe<GeographyPoint>> GeocodeAsync(Address address)
		{
			var encodedAddress = GetEncodedAddress(address);
			string formattedQueryUrl = GetServiceUrl(encodedAddress);

			var queryResults = await RunQueryAsync(formattedQueryUrl);

			return queryResults.Location;
		}

		private async Task<GoogleGeocodingResults> RunQueryAsync(string formattedQueryUrl)
		{
			using (var client = new HttpClient())
			{
				using (var result = await client.GetAsync(formattedQueryUrl))
				{

					var resultContent = await result.Content.ReadAsStringAsync();

					return new GoogleGeocodingResults(resultContent);
				}
			}
		}

		private string GetEncodedAddress(Address address)
			=> WebUtility.HtmlEncode(address.ToString());

		private string GetServiceUrl(string encodedAddress)
			=> serviceUrl.Replace("{address}", encodedAddress);
	}
}
