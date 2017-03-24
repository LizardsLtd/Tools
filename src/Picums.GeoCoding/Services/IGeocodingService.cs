using System.Threading.Tasks;
using Microsoft.Spatial;
using Picums.Localisation.Entities;
using Picums.Maybe;

namespace Picums.Localisation.Services
{
	public interface IGeocodingService
	{
		Task<Maybe<GeographyPoint>> GeocodeAsync(Address address);
	}
}