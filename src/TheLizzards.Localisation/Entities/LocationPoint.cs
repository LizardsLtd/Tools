using System;
using System.Collections.Generic;
using System.Text;

namespace TheLizzards.Localisation.Entities
{
	public sealed class LocationPoint
	{

		public LocationPoint(double latitude, double longitude)
		{
			this.Latitude = latitude;
			this.Longitude = longitude;
		}

		public double Latitude { get; }

		public double Longitude { get; }
	}
}
