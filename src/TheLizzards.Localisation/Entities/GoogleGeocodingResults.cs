using System;
using System.Linq;
using System.Xml.Linq;

namespace TheLizzards.Localisation.Entities
{
	internal sealed class GoogleGeocodingResults
	{
		public GoogleGeocodingResults(string xmlDocument)
		{
			var document = XDocument.Parse(xmlDocument);

			this.Status = LoadStatus(document);

			if (HasResults)
			{
				this.Location = LoadLocation(document);
			}
		}

		private string LoadStatus(XDocument document)
			=> document
				.Document
				.Descendants(XName.Get("status"))
				.First()
				.Value;

		public bool HasResults => Status == "OK";
		public bool HasNoResults => !HasResults;

		private LocationPoint LoadLocation(XDocument document)
		{
			var location = document
				.Document
				.Descendants(XName.Get("location"))
				.First();
			var latitude = double.Parse(location
				.Descendants(XName.Get("lat"))
				.First()
				.Value);
			var longitude = double.Parse(location
				.Descendants(XName.Get("lng"))
				.First()
				.Value);

			return new LocationPoint(latitude, longitude);
		}

		public string Status { get; }

		public LocationPoint Location { get; }
	}
}
