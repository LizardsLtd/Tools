using System.Threading.Tasks;

namespace TheLizzards.Data.CQRS
{
	public interface IAsyncQuery<TPayload> : IQuery<Task<TPayload>>
	{
	}
}