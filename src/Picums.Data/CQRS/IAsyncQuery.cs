using System.Threading.Tasks;

namespace Picums.Data.CQRS
{
    public interface IAsyncQuery<TPayload> : IQuery<Task<TPayload>>
    {
    }
}