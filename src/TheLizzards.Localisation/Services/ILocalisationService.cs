using System.Threading.Tasks;
using TheLizzards.Localisation.Entities;

namespace TheLizzards.Localisation.Services
{
	public interface ILocalisationService
    {
		Task<LocationPoint> Convert(Address address);
	}
}
