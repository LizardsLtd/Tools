namespace Picums.Search.Azure.KeyWords
{
    public sealed class LocationSearchParameter : ISearchForParameter
    {
        private readonly double Longitude;
        private readonly double Latitude;

        public LocationSearchParameter(double longitude, double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        public string GetSearchCommmand()
            => $"geo.distance({this.Longitude}, {this.Latitude})";
    }
}