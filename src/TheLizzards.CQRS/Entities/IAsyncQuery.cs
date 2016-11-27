using System.Threading.Tasks;

namespace TheLizzards.CQRS
{
	public interface IAsyncQuery<TPayload> : IQuery<Task<TPayload>>
	{
	}
}