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
		[Fact]
		public async Task ConnectToService()
		{
			var service = new GoogleGeocodingService();

			var result = await service.GeocodeAsync(
				houseNumber: "10"
				, streetName: "Downing street"
				, city: "London"
				, country: "UK");

			Assert.Equal(51.5033635, result.Latitude);
			Assert.Equal(-0.1276248, result.Longitude);
		}
	}
}
