using System.Threading.Tasks;
using Picums.Localisation.Services;
using Picums.Tests;
using Xunit;

namespace Picums.Location.Tests.Services
{
	public sealed class GoogleGeocodingServiceTests
	{
		private readonly GoogleGeocodingService service;

		public GoogleGeocodingServiceTests()
		{
			this.service = new GoogleGeocodingService(new TestLoggerFactory());
		}

		[Fact]
		public async Task ConnectToService()
		{
			var result = await this.service.GeocodeAsync(
				houseNumber: "10"
				, streetName: "Downing street"
				, city: "London"
				, country: "UK");

			Assert.Equal(51.5033635, result.Value.Latitude);
			Assert.Equal(-0.1276248, result.Value.Longitude);
		}

		[Fact]
		public async Task TryToChekcKnownAddress()
		{
			var result = await this.service.GeocodeAsync(
				houseNumber: "5"
				, streetName: "Kochanowskiego"
				, district: ""
				, city: "Zgorzelec"
				, province: "Dolnoslaskie"
				, country: "poland"
				, postCode: "50-900");

			Assert.Equal(51.1287637, result.Value.Latitude);
			Assert.Equal(15.0137526, result.Value.Longitude);
		}

		[Fact]
		public async Task TryToCheckNonExistingAddress()
		{
			var result = await this.service.GeocodeAsync(
				houseNumber: ""
				, streetName: ""
				, country: "");

			Assert.True(result.IsNone);
		}
	}
}