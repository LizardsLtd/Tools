using System.Threading.Tasks;
using Microsoft.Spatial;
using Picums.Maybe;

namespace Picums.GeoCoding
{
    public interface IGeocodingService
    {
        Task<Maybe<GeographyPoint>> GeocodeAsync(Address address);
    }
}