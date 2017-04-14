using Microsoft.Spatial;
using Picums.Maybe;
using System.Threading.Tasks;

namespace Picums.GeoCoding
{
    public interface IGeocodingService
    {
        Task<Maybe<GeographyPoint>> GeocodeAsync(Address address);
    }
}