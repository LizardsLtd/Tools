namespace Picums.Search.Azure.KeyWords
{
    public sealed class LocationKeyword : ISearchForParameter
    {
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
        }

        public string GetSearchCommmand()
            => $"$filter=geo.distance({locationField}, geography'POINT({this.Longitude} {this.Latitude})') lt {distanceInKilometers}";
    }
}