using TheLizzards.Localisation.Entities;

namespace TheLizzards.Localisation.Services
{
	public interface ILocalisationService
    {
		LocationPoint Convert(Address address);
	}
}
