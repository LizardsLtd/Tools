using System;
using System.Collections.Generic;
using System.Text;

namespace TheLizzards.Search.Entities
{
	public sealed class LocationSearchKeyWord: ISearchKey
	{
		public LocationSearchKeyWord(double latitute, double longitude)
		{
			this.Latitude = latitute;
			this.Longitude = longitude;
		}

		public double Latitude { get; }

		public double Longitude { get; }
	}
}
