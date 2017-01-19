using System.Threading.Tasks;
using TheLizzards.Localisation.Entities;
using TheLizzards.Maybe;
using Microsoft.Spatial;

namespace TheLizzards.Localisation.Services
{
	public interface IGeocodingService
    	{
		Task<Maybe<GeographyPoint>> GeocodeAsync(Address address);
	}
}
