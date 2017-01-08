using System;
using System.Collections.Generic;
using System.Text;
using TheLizzards.Localisation.Entities;

namespace TheLizzards.Localisation.Services
{
	public sealed class GoogleLocalisationService : ILocalisationService
	{
		private readonly string apiKey;

		public GoogleLocalisationService(string apiKey)
		{
			this.apiKey = apiKey;
		}

		public LocationPoint Convert(Address address)
		{
			throw new NotImplementedException();
		}
	}
}
