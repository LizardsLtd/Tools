using System.Threading.Tasks;
using Microsoft.Spatial;
using TheLizzards.Localisation.Entities;
using TheLizzards.Maybe;

namespace TheLizzards.Localisation.Services
{
	public interface IGeocodingService
	{
		Task<Maybe<GeographyPoint>> GeocodeAsync(Address address);
	}
}