using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Spatial;
using NLog;
using Picums.Maybe;

namespace Picums.GeoCoding
{
    /// <summary>
    /// Service to convert address to spatial coordiantes
    /// </summary>
    public sealed class GoogleGeocodingService : IGeocodingService
    {
        private const string serviceUrl = "http://maps.google.com/maps/api/geocode/xml?address={address}&sensor=false";
        private readonly ILogger logger;

        public GoogleGeocodingService(ILogger logger)
        {
            this.logger = logger;
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
                    this.logger.Debug($"Geocoding results {resultContent}");
                    return new GoogleGeocodingResults(this.logger, resultContent);
                }
            }
        }

        private string GetEncodedAddress(Address address)
        {
            var encodedAddress = WebUtility.HtmlEncode(address.ToString());
            this.logger.Debug($"Geocoding for address {encodedAddress}");
            return encodedAddress;
        }

        private string GetServiceUrl(string encodedAddress)
            => serviceUrl.Replace("{address}", encodedAddress);
    }
}