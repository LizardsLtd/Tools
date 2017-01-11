using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheLizzards.Localisation.Services;
using Xunit;

namespace TheLizzards.Location.Tests.Services
{
	public sealed class GoogleGeocodingServiceTests
	{
		private readonly GoogleGeocodingService service;

		public GoogleGeocodingServiceTests()
		{
			this.service = new GoogleGeocodingService();
		}

		[Fact]
		public async Task ConnectToService()
		{
			var result = await this.service.GeocodeAsync(
				houseNumber: "10"
				, streetName: "Downing street"
				, city: "London"
				, country: "UK");

			Assert.Equal(51.5033635, result.Latitude);
			Assert.Equal(-0.1276248, result.Longitude);
		}

		[Fact]
		public async Task TryToCheckNonExistingAddress()
		{
			var result = await this.service.GeocodeAsync(
				   houseNumber: "blah "
				   , streetName: "blha"
				   , city: "bla"
				   , country: "UK");
		}
	}
}
