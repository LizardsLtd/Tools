using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Spatial;
using Microsoft.Extensions.Logging;

namespace TheLizzards.Localisation.Entities
{
	internal sealed class GoogleGeocodingResults
	{
		private readonly ILogger logger;

		public GoogleGeocodingResults(ILogger logger, string xmlDocument)
		{
			this.logger = logger;
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

		private GeographyPoint LoadLocation(XDocument document)
		{
			var location = document
				.Document
				.Descendants(XName.Get("location"))
				.First();
			var latitude = GetProperty(location, "lat");
			var longitude = GetProperty(location, "lng");

			return GeographyPoint.Create((double)latitude, (double)longitude);
		}

		private double GetProperty(XElement location, string key)
		{
			var latitudeAsString = location
				.Descendants(XName.Get(key))
				.First()
				.Value;
			this.logger.LogDebug($"{key}: {latitudeAsString}");

			var value = decimal.Parse(latitudeAsString);
			return (double)value;
		}

		public string Status { get; }

		public GeographyPoint Location { get; }
	}
}
