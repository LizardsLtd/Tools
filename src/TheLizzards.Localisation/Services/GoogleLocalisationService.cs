codin﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheLizzards.Localisation.Entities;

namespace TheLizzards.Localisation.Services
{
	public sealed class GoogleGeocodingService : ILocalisationService
	{
		private readonly string apiKey;
		private const string serviceUrl = "http://maps.google.com/maps/api/geocode/xml?address={address}&sensor=false";

		public GoogleLocalisationService(string apiKey)
		{
			this.apiKey = apiKey;
		}

		public async Task<LocationPoint> Convert(Address address)
		{
			var formattedQueryUrl =
				serviceUrl
				.Replace("{address}", address.ToString())
				.Replace("{key}", apiKey);

			var client = new HttpClient();

			var result = await client.GetAsync(formattedQueryUrl);
			return null;
		}
	}
}
