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

            Assert.Equal(51.5033635, result.Value.Latitude);
            Assert.Equal(-0.1276248, result.Value.Longitude);
        }

        [Fact]
        public async Task TryToChekcKNwnAddress()
        {
            var result = await this.service.GeocodeAsync(
                houseNumber: "5"
                , streetName: "Kochanowskiego"
                , district: ""
                , city: "Zgorzelec"
                , province: "Dolnoslaskie"
                , country: "poland"
                , postCode:"50-900");

            Assert.Equal(51.5033635, result.Value.Latitude);
            Assert.Equal(-0.1276248, result.Value.Longitude);
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
