using System.Globalization;

namespace Picums.Search.Azure.KeyWords
{
    public sealed class LocationFilter : IFilter
    {
        private readonly CultureInfo AzureRedableCulture;

        private readonly double Longitude;
        private readonly double Latitude;
        private readonly double distanceInKilometers;
        private readonly string locationField;

        public LocationFilter(double longitude, double latitude, string locationField, int distanceInKilometers = 25)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.locationField = locationField;
            this.distanceInKilometers = distanceInKilometers;
            this.AzureRedableCulture = new CultureInfo("en-US");
        }

        private string UsFormattedLongitude
            => this.Longitude.ToString(this.AzureRedableCulture);

        private string UsFormattedLatitude
            => this.Latitude.ToString(this.AzureRedableCulture);

        private string ConvertLatAndLongToGeographyPoint
            => $" geography'POINT({this.UsFormattedLongitude} {this.UsFormattedLatitude})'";

        public string GetFilter()
            => $"geo.distance({locationField},{this.ConvertLatAndLongToGeographyPoint}) lt {distanceInKilometers}";
    }
}