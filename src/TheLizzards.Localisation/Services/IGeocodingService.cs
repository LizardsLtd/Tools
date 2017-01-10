using System.Threading.Tasks;
using TheLizzards.Localisation.Entities;

namespace TheLizzards.Localisation.Services
{
	public interface IGeocodingService
    {
		Task<LocationPoint> GeocodeAsync(Address address);
	}
}
