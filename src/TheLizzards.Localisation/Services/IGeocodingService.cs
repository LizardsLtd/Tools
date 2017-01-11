using System.Threading.Tasks;
using TheLizzards.Localisation.Entities;
using TheLizzards.Maybe;

namespace TheLizzards.Localisation.Services
{
	public interface IGeocodingService
    {
		Task<Maybe<LocationPoint>> GeocodeAsync(Address address);
	}
}
