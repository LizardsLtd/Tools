using System.Globalization;

namespace Picums.Search.Azure.KeyWords
{
    public sealed class LocationKeyword : ISearchForParameter
    {
        private readonly CultureInfo AzureRedableCulture;

        private readonly double Longitude;
        private readonly double Latitude;
        private readonly double distanceInKilometers;
        private readonly string locationField;

        public LocationKeyword(double longitude, double latitude, string locationField, double distanceInKilometers = 25)
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

        public string GetSearchCommmand()
            => $"$filter=geo.distance({locationField},{this.ConvertLatAndLongToGeographyPoint}) lt {distanceInKilometers}";
    }
}